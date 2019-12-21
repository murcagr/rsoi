using Microsoft.AspNetCore.Authentication;
using FoodService.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Authentication
{
    public static class AuthenticationBuilderExtensions
    {
        // Custom authentication extension method
        public static AuthenticationBuilder AddFoodAuth(this AuthenticationBuilder builder, Action<FoodAuthOptions> configureOptions)
        {
            // Add custom authentication scheme with custom options and custom handler
            return builder.AddScheme<FoodAuthOptions, FoodAuthHandler>(FoodAuthOptions.DefaultScheme, configureOptions);
        }
    }
}
