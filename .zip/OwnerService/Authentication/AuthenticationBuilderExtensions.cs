using Microsoft.AspNetCore.Authentication;
using OwnerService.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OwnerService.Authentication
{
    public static class AuthenticationBuilderExtensions
    {
        // Custom authentication extension method
        public static AuthenticationBuilder AddOwnerAuth(this AuthenticationBuilder builder, Action<OwnerAuthOptions> configureOptions)
        {
            // Add custom authentication scheme with custom options and custom handler
            return builder.AddScheme<OwnerAuthOptions, OwnerAuthHandler>(OwnerAuthOptions.DefaultScheme, configureOptions);
        }
    }
}
