using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.Model;

namespace FoodService.Services
{
    public interface IFoodServ
    {
        Task<Food> AddFood(Food owner);
        Task<Food> DeleteFoodById(int id);
        Task<Food> GetFoodByIdAsync(int ownerId);
        Task<IEnumerable<Food>> GetFoodsAsync();
    }
}