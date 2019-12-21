using System.Collections.Generic;
using System.Threading.Tasks;
using CatService.Model;

namespace CatService.Services
{
    public interface ICatServ
    {
        Task<Cat> AddCat(Cat cat);
        Task<Cat> DeleteCatById(int id);
        Task<Cat> GetCatByIdAsync(int id);
        Task<IEnumerable<Cat>> GetCatByOwnerIdAsync(int ownerId);
        Task<IEnumerable<Cat>> GetCatsAsync(int page, int pageSize);
        Task<IEnumerable<Cat>> DeleteAllCatsByOwner(int id);
    }
}