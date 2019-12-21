using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodService.Model;
using FoodService.Services;
using ApiGateway.Models;
using FoodService.Model.DB;
using Microsoft.AspNetCore.Authorization;
using FoodService.Storage;

namespace FoodService.Controllers
{
    [Route("api/foods")]
    [Authorize(Policy = "Gateway")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private IFoodServ _foodService;
        private TokenStorage _tokenStorage;

        public FoodController(IFoodServ foodService, TokenStorage tokenStorage)
        {
            _foodService = foodService;
            _tokenStorage = tokenStorage;
        }

        // GET api/foods
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var foods = await _foodService.GetFoodsAsync();

            return Ok(foods);
        }

        // GET api/foods
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var foods = await _foodService.GetFoodByIdAsync(id);

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


        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthOwner([FromBody] AuthModel auth)
        {
            if (auth.AppId == 3 && auth.AppSecret == "foodApp")
            {
                int expiration = 3600;
                var token = $"{(Int32)(DateTime.UtcNow.AddSeconds(expiration).Subtract(new DateTime(1970, 1, 1))).TotalSeconds}.{Guid.NewGuid().ToString()}";
                _tokenStorage.activeTokens.Add(token);
                return Ok(new TokenM() { Token = token, Exp_in = expiration });
            }
            else
                return StatusCode(StatusCodes.Status401Unauthorized);
        }

        [HttpGet("status")]
        [AllowAnonymous]
        public async Task<IActionResult> HealthCheck()
        {
            return Ok();
        }
    }
}
