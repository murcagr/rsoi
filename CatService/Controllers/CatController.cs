using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatService.Model;
using CatService.Services;
using Microsoft.AspNetCore.Authorization;
using CatService.Model.DB;
using ApiGateway.Models;
using CatService.Storage;

namespace CatService.Controllers
{
    [Route("api/cats")]
    //[Authorize(Policy = "Gateway")]
    [ApiController]
    public class CatController : ControllerBase, ICatController
    {
        private ICatServ _catService;
        private TokenStorage _tokenStorage;

        public CatController(ICatServ catService, TokenStorage tokenStorage)
        {
            _catService = catService;
            _tokenStorage = tokenStorage;
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

        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthCat([FromBody] AuthModel auth)
        {
            if (auth.AppId == 2 && auth.AppSecret == "catApp")
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
