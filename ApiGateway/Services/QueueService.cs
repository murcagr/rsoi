using ApiGateway.Models;
using ApiGateway.Clients;
using ApiGateway.Queue;
using Microsoft.Extensions.Hosting;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateway.Services
{
    public class QueueService : BackgroundService
    {
        private const int TIME_DELAY = 1000;

        private readonly  CatQueue _catQueue;
        private readonly OwnerQueue _ownerQueue;
        private readonly ICatClient _catClient;
        private readonly IOwnerClient _ownerClient;

        public QueueService(CatQueue catQueue, OwnerQueue ownerQueue, 
            ICatClient catClient, IOwnerClient ownerClient)
        {
            _catQueue = catQueue;
            _ownerQueue = ownerQueue;
            _ownerClient = ownerClient;
            _catClient = catClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TIME_DELAY);

                Console.WriteLine("Execute TryAttackMonster!");
                await TryAttackMonster();
            }
        }

        private async Task TryAttackMonster()
        {
            try
            {
                if (!await _ownerClient.HealthCheck())
                {
                    Console.WriteLine("Owner service is unavailable");
                    return;
                }
            }
            catch (BrokenCircuitException)
            {
                Console.WriteLine("Owner service is unavailable");
                return;
            }

            try
            {
                if (!await _catClient.HealthCheck())
                {
                    Console.WriteLine("Cat service is unavailable");
                    return;
                }
            }
            catch (BrokenCircuitException)
            {
                Console.WriteLine("Cat service is unavailable");
                return;
            }

            while (_ownerQueue.OwnerDeleteQueueTasks.Count != 0 && _catQueue.CatDeleteQueueTasks.Count != 0)
            {
                
                if (_ownerQueue.OwnerDeleteQueueTasks.TryDequeue(out var ownerId))
                {
                    
                    try
                    {
                        await _ownerClient.DeleteOwner(ownerId);
                    }
                    catch (BrokenCircuitException)
                    {
                        _ownerQueue.OwnerDeleteQueueTasks.Enqueue(ownerId);
                    }
                }
                if (_catQueue.CatDeleteQueueTasks.TryDequeue(out var catId))
                {
                   try
                    {
                        await _catClient.DeleteCat(catId);
                    }
                    catch (BrokenCircuitException)
                    {
                        _catQueue.CatDeleteQueueTasks.Enqueue(catId);
                    }
                }
            }
        }
    }
}