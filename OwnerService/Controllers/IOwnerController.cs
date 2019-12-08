using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OwnerService.Model;

namespace OwnerService.Controllers
{
    public interface IOwnerController
    {
        Task<IActionResult> AddOwner([FromBody] Owner owner);
        Task<IActionResult> DeleteOwner(int id);
        Task<IActionResult> GetOwnerByIdAsync(int id);
        Task<IActionResult> GetOwners();
        Task<IActionResult> HealthCheck();
    }
}