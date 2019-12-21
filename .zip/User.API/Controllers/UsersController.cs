using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.API.Exceptions;
using User.API.Models;
using User.API.Services;

namespace User.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        
        [HttpGet("token")]
        public async Task<IActionResult> CheckToken()
        {
            return Ok("Token correct!");
        }

     

        [HttpPost("reg")]
        public async Task<IActionResult> Register([FromBody] Login data)
        {
            try
            {
                var user = await _usersService.RegisterUser(data.Username, data.Password);

                return Ok(user);
            }
            catch (RegisterException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById([FromQuery] string id)
        {
            try
            {
                var user = await _usersService.GetUserById(id);

                return Ok(user);
            }
            catch (NoSuchUserException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }
    }
}
