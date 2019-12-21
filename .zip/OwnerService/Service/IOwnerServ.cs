using System.Collections.Generic;
using System.Threading.Tasks;
using OwnerService.Model;

namespace OwnerService.Services
{
    public interface IOwnerServ
    {
        Task<Owner> AddOwner(Owner owner);
        Task<Owner> DeleteOwnerByID(int id);
        Task<Owner> GetOwnerByIdAsync(int id);
        Task<IEnumerable<Owner>> GetOwners();
    }
}