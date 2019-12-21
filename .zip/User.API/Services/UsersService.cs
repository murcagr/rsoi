using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using User.API.Configs;
using User.API.Exceptions;
using User.API.Models;

namespace User.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly JwtIssuerOptions _jwtOptions;

        public UsersService(UserManager<IdentityUser> userManager, 
            IOptions<AppSettings> appSettingsAccessor)
        {
            _userManager = userManager;
            _appSettings = appSettingsAccessor.Value;
        }

       
        public async Task<IdentityUser> RegisterUser(string username, string pwd)
        {
            IdentityUser user = new IdentityUser
            { UserName = username
            };

            IdentityResult result = await _userManager.CreateAsync(user, pwd);

            if (!result.Succeeded)
                throw new RegisterException(result.Errors.First().Description);

            return user;
        }

        public async Task<IdentityUser> GetUserById(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);

            if (user != null)
                return user;
            else
                throw new NoSuchUserException("No such user!");
        }


    }
}
