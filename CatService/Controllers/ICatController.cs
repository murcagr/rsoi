using System.Threading.Tasks;
using CatService.Model;
using Microsoft.AspNetCore.Mvc;

namespace CatService.Controllers
{
    public interface ICatController
    {
        Task<IActionResult> AddCat([FromBody] Cat cat);
        Task<IActionResult> DeleteCat(int id);
        Task<IActionResult> Get([FromQuery] int page = 0, int pageSize = 10);
        Task<IActionResult> GetCatById(int id);
        Task<IActionResult> GetCatsByOwnerIdAsync(int ownerId);
        Task<IActionResult> HealthCheck();
        
    }
}