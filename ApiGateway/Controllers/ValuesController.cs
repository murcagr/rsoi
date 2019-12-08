using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiGateway.Services;
using ApiGateway.Models;
using Polly.CircuitBreaker;
using Microsoft.AspNetCore.Http;
using ApiGateway.Exceptions;

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
            try
            {
                var owner = await _gwService.GetOwners();

                return Ok(owner);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }

        [HttpGet("owners/{id}")]
        public async Task<IActionResult> GetOwnerById(int id)
        {
            try
            {
                var owner = await _gwService.GetOwnerByIdAsync(id);

                return Ok(owner);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }



        // POST api/values
        [HttpPost("owners")]
        public async Task<IActionResult> AddOwnerAsync([FromBody] Owner owner)
        {
            try
            {
                var _owner = await _gwService.AddUser(owner);

                return Ok(_owner);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }


        // CATS
        [HttpGet("cats")]
        public async Task<IActionResult> GetCats()
        {
            try
            {
                var _cats = await _gwService.GetCats();

                return Ok(_cats);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }

        [HttpGet("cats/{catId}")]
        public async Task<IActionResult> GetCats(int catId)
        {
            try
            {
                var _cats = await _gwService.GetCatsByIdAsync(catId);

                return Ok(_cats);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }

        [HttpGet("cats/owner/{ownerId}")]
        public async Task<IActionResult> GetCatsByOwner(int ownerId)
        {
            try
            {
                var _cats = await _gwService.GetCatsByOwnerIdAsync(ownerId);
                return Ok(_cats);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }

        [HttpPost("cats")]
        public async Task<IActionResult> AddCat([FromBody] Cat cat)
        {
            try
            {
                var _cat = await _gwService.AddCat(cat);

                return Ok(_cat);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }

        [HttpDelete("cats/{id}")]
        public async Task<IActionResult> DeleteCat(int id)
        {
            try
            {
                var _cat = await _gwService.DeleteCat(id);

                return Ok(_cat);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }

        // FOOD 
        [HttpGet("foods")]
        public async Task<IActionResult> GetFood()
        {
            try
            {
                var _cats = await _gwService.GetFood();

                return Ok(_cats);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }


        [HttpGet("foods/{id}")]
        public async Task<IActionResult> GetFoodById(int id)
        {
            try
            {
                var _cat = await _gwService.GetFoodById(id);

                return Ok(_cat);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }

        [HttpDelete("foods/{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            try
            {
                var _cat = await _gwService.DeleteFood(id);

                return Ok(_cat);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }

        // OWNER CATS
        [HttpGet("ownercats/{id}")]
        public async Task<IActionResult> GetOwnerAndCats(int id)
        {
            try
            {
                var _cat = await _gwService.GetOwnerAndCats(id);
                return Ok(_cat);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }

        [HttpPost("ownercats")]
        public async Task<IActionResult> AddOwnerAndCat([FromBody] OwnerCat ownerCats)
        {
            try
            {
                var _cat = await _gwService.AddOwnerAndCat(ownerCats);
                return Ok(_cat);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
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
            try
            {
                var _cat = await _gwService.AddFoodOwnerCat(cof);

                return Ok(_cat);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }

        [HttpGet("ownercatfood")]
        public async Task<IActionResult> GetCatOwnerFood([FromQuery] int page, int pageSize)
        {
            try
            {
                var _cat = await _gwService.GetCatOwnerFood(page, pageSize);

                return Ok(_cat);
            }
            catch (RequestException e)
            {
                return BadRequest(new ErrorResponse() { Error = e.Message });
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    Error = "Service is unreachable. Please try later."
                });
            }
            catch (InternalException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse() { Error = e.Message });
            }
        }

    }
}
