using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OwnerService.Model;
using OwnerService.Services;

namespace OwnerService.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnerController : ControllerBase, IOwnerController
    {
        private IOwnerServ _ownerService;

        public OwnerController(IOwnerServ ownerService)
        {
            _ownerService = ownerService;
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
    }
}
