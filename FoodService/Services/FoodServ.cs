using FoodService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services
{
    public class FoodServ : IFoodServ
    {
        private readonly AppDBContext _AppDBContext;
        public FoodServ(AppDBContext appDBContext)
        {
            _AppDBContext = appDBContext;
        }



        public async Task<IEnumerable<Food>> GetFoodsAsync()
        {
            var owner = _AppDBContext.Foods.ToList();

            return owner;
        }

        public async Task<Food> AddFood(Food owner)
        {
            var resp = _AppDBContext.Foods.Add(owner);
            await _AppDBContext.SaveChangesAsync();

            return owner;

        }

        public async Task<Food> DeleteFoodById(int id)
        {

            var owner = _AppDBContext.Foods.SingleOrDefault(c => c.Id == id);
            if (owner != null)
            {
                _AppDBContext.Foods.Remove(owner);
                await _AppDBContext.SaveChangesAsync();
                return owner;
            }

            return null;
            //return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

        public async Task<Food> GetFoodByIdAsync(int id)
        {
            var owner = _AppDBContext.Foods.SingleOrDefault(c => c.Id == id);

            return owner;
        }

    }
}
