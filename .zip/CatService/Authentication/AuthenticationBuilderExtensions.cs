using Microsoft.AspNetCore.Authentication;
using CatService.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatService.Authentication
{
    public static class AuthenticationBuilderExtensions
    {
        // Custom authentication extension method
        public static AuthenticationBuilder AddCatAuth(this AuthenticationBuilder builder, Action<CatAuthOptions> configureOptions)
        {
            // Add custom authentication scheme with custom options and custom handler
            return builder.AddScheme<CatAuthOptions, CatAuthHandler>(CatAuthOptions.DefaultScheme, configureOptions);
        }
    }
}
