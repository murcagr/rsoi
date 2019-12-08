using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiGateway.Services;
using ApiGateway.Models;

namespace ApiGateway.Controllers
{
    [Route("api/gw")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private GwService _gwService;

        public ValuesController(GwService gwService)
        {
            _gwService = gwService;
        }


        // OWNERS

        // GET api/values
        [HttpGet("owners")]
        public async Task<IActionResult> Get()
        {
            var owner = await _gwService.GetOwners();

            return Ok(owner);
        }

        [HttpGet("owners/{id}")]
        public async Task<IActionResult> GetOwnerById(int id)
        {
            var owner = await _gwService.GetOwnerByIdAsync(id);

            return Ok(owner);
        }



        // POST api/values
        [HttpPost("owners")]
        public async Task<IActionResult> AddOwnerAsync([FromBody] Owner owner)
        {

            var _owner = await _gwService.AddUser(owner);

            return Ok(_owner);
        }


        // CATS
        [HttpGet("cats")]
        public async Task<IActionResult> GetCats()
        {

            var _cats = await _gwService.GetCats();

            return Ok(_cats);
        }

        [HttpGet("cats/{catId}")]
        public async Task<IActionResult> GetCats(int catId)
        {

            var _cats = await _gwService.GetCatsByIdAsync(catId);

            return Ok(_cats);
        }

        [HttpGet("cats/owner/{ownerId}")]
        public async Task<IActionResult> GetCatsByOwner(int ownerId)
        {

            var _cats = await _gwService.GetCatsByOwnerIdAsync(ownerId);

            return Ok(_cats);
        }

        [HttpPost("cats")]
        public async Task<IActionResult> AddCat([FromBody] Cat cat)
        {

            var _cat = await _gwService.AddCat(cat);

            return Ok(_cat);
        }

        [HttpDelete("cats/{id}")]
        public async Task<IActionResult> DeleteCat(int id)
        {

            var _cat = await _gwService.DeleteCat(id);

            return Ok(_cat);
        }

        // FOOD 
        [HttpGet("foods")]
        public async Task<IActionResult> GetFood()
        {

            var _cats = await _gwService.GetFood();

            return Ok(_cats);
        }


        [HttpGet("foods/{id}")]
        public async Task<IActionResult> GetFoodById(int id)
        {

            var _cat = await _gwService.GetFoodById(id);

            return Ok(_cat);
        }

        [HttpDelete("foods/{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {

            var _cat = await _gwService.DeleteFood(id);

            return Ok(_cat);
        }

        // OWNER CATS
        [HttpGet("ownercats/{id}")]
        public async Task<IActionResult> GetOwnerAndCats(int id)
        {

            var _cat = await _gwService.GetOwnerAndCats(id);

            return Ok(_cat);
        }

        [HttpPost("ownercats")]
        public async Task<IActionResult> AddOwnerAndCat([FromBody] OwnerCat ownerCats)
        {

            var _cat = await _gwService.AddOwnerAndCat(ownerCats);

            return Ok(_cat);
        }

        
        [HttpDelete("ownercats/{id}")]
        public async Task<IActionResult> DeleteOwnerandHisCats(int id)
        {
            var _cat = await _gwService.DeleteOwnerandHisCats(id);

            return Ok(_cat);
        }

        [HttpPost("ownercatfood")]
        public async Task<IActionResult> AddFoodOwnerCat([FromBody] CatOwnerFood cof)
        {

            var _cat = await _gwService.AddFoodOwnerCat(cof);

            return Ok(_cat);
        }

        [HttpGet("ownercatfood")]
        public async Task<IActionResult> GetCatOwnerFood([FromQuery] int page, int pageSize)
        {

            var _cat = await _gwService.GetCatOwnerFood(page, pageSize);

            return Ok(_cat);
        }

    }
}
