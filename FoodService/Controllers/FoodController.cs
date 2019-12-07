using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodService.Model;
using FoodService.Services;

namespace FoodService.Controllers
{
    [Route("api/foods")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private IFoodServ _foodService;

        public FoodController(IFoodServ foodService)
        {
            _foodService = foodService;
        }

        // GET api/foods
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var foods = await _foodService.GetFoodsAsync();

            return Ok(foods);
        }

        // POST api/foods
        [HttpPost]
        public async Task<IActionResult> AddFood([FromBody] Food food)
        {
            try
            {
                food = await _foodService.AddFood(food);

                return Ok(food);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            try
            {
                var food = await _foodService.DeleteFoodById(id);

                return Ok(food);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
