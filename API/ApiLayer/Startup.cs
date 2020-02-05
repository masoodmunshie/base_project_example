using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ApiLayer.StartupExtensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using DAL.Interfaces;
using DAL.Service;

namespace ApiLayer
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
            services.AddControllers();
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.AddDbContext<DBContext>(options =>options.UseNpgsql(Configuration.GetConnectionString("sqlConnection")));
            services.InterfaceServices();
            services.JWTAuthorization();


            var sp = services.BuildServiceProvider();
            var authService = sp.GetService<Iauth>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("roles", policy =>
                    policy.Requirements.Add(new auth_handler_requirement(21, authService)));
            });

            
            services.AddSingleton<IAuthorizationHandler, RolesHandler>();

           
            




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
