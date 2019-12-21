using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatService.Authentication
{
    public class CatAuthOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "CatAuth";
        public string Scheme => DefaultScheme;
    }
}
