using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnerService.Authentication
{
    public class OwnerAuthOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "OwnerAuth";
        public string Scheme => DefaultScheme;
    }
}
