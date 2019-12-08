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
            services.AddTransient<GwService>();
            services.AddHttpClient<ICatClient, CatClient>(client =>
            {
                client.BaseAddress = new Uri(connectionsSection.Get<Connections>().CatsAPIUrl);
            });

            services.AddHttpClient<IOwnerClient, OwnerClient>(client =>
            {
                client.BaseAddress = new Uri(connectionsSection.Get<Connections>().OwnersApiUrl);
            });
            services.AddHttpClient<IFoodClient, FoodClient>(client =>
            {
                client.BaseAddress = new Uri(connectionsSection.Get<Connections>().FoodsAPIUrl);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
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
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
