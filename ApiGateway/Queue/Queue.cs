using ApiGateway.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Queue
{
    public class OwnerQueue
    {
        public ConcurrentQueue<int> OwnerDeleteQueueTasks { get; set; } = new ConcurrentQueue<int>();
    }

    public class CatQueue
    {
        public ConcurrentQueue<int> CatDeleteQueueTasks { get; set; } = new ConcurrentQueue<int>();
    }
}