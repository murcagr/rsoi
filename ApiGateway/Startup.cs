using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApiGateway.Clients;
using ApiGateway.Services;
using ApiGateway.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using ApiGateway.Queue;
using ApiGateway.Settings;
using ApiGateway.Models;
using static ApiGateway.Settings.AuthService;
using Microsoft.IdentityModel.Logging;

namespace ApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionsSection = Configuration.GetSection("Connections");
            
            services.Configure<Connections>(connectionsSection);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
                 .AddIdentityServerAuthentication(options =>
                 {
                     options.Authority = "http://user-service/";
                     options.ApiName = "api1";
                     options.RequireHttpsMetadata = false;
                     options.JwtValidationClockSkew = TimeSpan.Zero;
                 });

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("User", policy => policy.RequireClaim());
            });

            services.AddSingleton<AuthToService>();
            services.AddSingleton(new CatQueue());
            services.AddSingleton(new OwnerQueue());
            services.AddHostedService<QueueService>();

            services.AddHttpClient<ICatClient, CatClient>(client =>
            {
                client.BaseAddress = new Uri(connectionsSection.Get<Connections>().CatsAPIUrl);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<IOwnerClient, OwnerClient>(client =>
            {
                client.BaseAddress = new Uri(connectionsSection.Get<Connections>().OwnersApiUrl);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<IFoodClient, FoodClient>(client =>
            {
                client.BaseAddress = new Uri(connectionsSection.Get<Connections>().FoodsAPIUrl);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            


            services.AddTransient<GwService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseMvc();
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30), (ex, dur) => { Console.WriteLine("CircuitBreaker opened"); }, () => { Console.WriteLine("CircuitBreaker reset"); });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine("Retry attempt!");
                });
        }
    }

}
