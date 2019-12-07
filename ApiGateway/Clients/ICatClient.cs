using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Models;
namespace ApiGateway.Clients
{
    public interface ICatClient
    {
        Task<IEnumerable<Cat>> GetCatsByOwnerIdAsync(int ownerId);
        Task<Cat> GetCatByIdAsync(int id);
        Task<IEnumerable<Cat>> GetCats();
        Task<Cat> AddNewCat(Cat cat);
        Task<bool> DeleteCat(int ownerId);


    }
}
