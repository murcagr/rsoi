using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Models;
namespace ApiGateway.Clients
{
    public interface IFoodClient
    {
        Task<Food> GetFoodByIdAsync(int id);
        Task<Food> AddNewFood(Food food);
        Task<bool> DeleteFood(int ownerId);
        Task<IEnumerable<Food>> GetFoods();
        Task<bool> HealthCheck();


    }
}
