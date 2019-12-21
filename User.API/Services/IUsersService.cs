using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.API.Models;

namespace User.API.Services
{
    public interface IUsersService
    {

        Task<IdentityUser> RegisterUser(string username, string pwd);
        Task<IdentityUser> GetUserById(string id);
    }
}
