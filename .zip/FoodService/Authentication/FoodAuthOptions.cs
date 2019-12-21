using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Authentication
{
    public class FoodAuthOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "FoodAuth";
        public string Scheme => DefaultScheme;
    }
}
