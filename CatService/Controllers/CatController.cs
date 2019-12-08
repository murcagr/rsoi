using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatService.Model;
using CatService.Services;

namespace CatService.Controllers
{
    [Route("api/cats")]
    [ApiController]
    public class CatController : ControllerBase, ICatController
    {
        private ICatServ _catService;

        public CatController(ICatServ catService)
        {
            _catService = catService;
        }

        // GET api/cats
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 0, int pageSize = 10)
        {
            IEnumerable<Cat> cats = await _catService.GetCatsAsync(page, pageSize);

            return Ok(cats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatById(int id)
        {
            Cat cat = await _catService.GetCatByIdAsync(id);

            if (cat != null)
                return Ok(cat);
            else
                return StatusCode(StatusCodes.Status400BadRequest, "No such cat!");
        }

        // GET api/cats/owner/5
        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetCatsByOwnerIdAsync(int ownerId)
        {
            var cats = await _catService.GetCatByOwnerIdAsync(ownerId);

            if (cats != null)
                return Ok(cats);
            else
                return StatusCode(StatusCodes.Status400BadRequest, "No such cat!");
        }

        [HttpDelete("owner/{ownerId}")]
        public async Task<IActionResult> DeleteAllCatsByOwner(int ownerId)
        {
            var cats = await _catService.DeleteAllCatsByOwner(ownerId);

            if (cats != null)
                return Ok(cats);
            else
                return StatusCode(StatusCodes.Status400BadRequest, "No such cat!");
        }

        // POST api/cats
        [HttpPost]
        public async Task<IActionResult> AddCat([FromBody] Cat cat)
        {
            try
            {
                cat = await _catService.AddCat(cat);

                return Ok(cat);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCat(int id)
        {
            try
            {
                var cat = await _catService.DeleteCatById(id);

                return Ok(cat);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("status")]
        public async Task<IActionResult> HealthCheck()
        {
            return Ok();
        }
    }
}
