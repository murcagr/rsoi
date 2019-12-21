using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OwnerService.Storage;
using OwnerService.Model;
using OwnerService.Model.DB;
using OwnerService.Services;

namespace OwnerService.Controllers
{
    [Route("api/owners")]
    [Authorize(Policy = "Gateway")]
    [ApiController]
    public class OwnerController : ControllerBase, IOwnerController
    {
        private IOwnerServ _ownerService;
        private readonly TokenStorage _tokenStorage;


        public OwnerController(IOwnerServ ownerService, TokenStorage tokenStorage)
        {
            _ownerService = ownerService;
            _tokenStorage = tokenStorage;
        }

        [HttpGet]
        public async Task<IActionResult> GetOwners()
        {
            try
            {
                var owners = await _ownerService.GetOwners();
                return Ok(owners);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        // GET api/Owners/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnerByIdAsync(int id)
        {
            try
            {
                var owners = await _ownerService.GetOwnerByIdAsync(id);
                return Ok(owners);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOwner([FromBody] Owner owner)
        {
            try
            {
                var _owner = await _ownerService.AddOwner(owner);

                return Ok(_owner);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            try
            {
                var owner = await _ownerService.DeleteOwnerByID(id);

                return Ok(owner);
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
            if (auth.AppId == 1 && auth.AppSecret == "ownerApp")
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
