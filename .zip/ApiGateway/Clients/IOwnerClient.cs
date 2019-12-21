using ApiGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Clients
{
    public interface IOwnerClient 
    {
        Task<Owner> GetOwnerByIdAsync(int id);
        Task<IEnumerable<Owner>> GetOwners();
        Task<Owner> AddOwner(Owner owner);
        Task<Owner> DeleteOwner(int id);
        Task<bool> HealthCheck();
        Task<bool> GetTokenCorrectness(string token);
    }
}
