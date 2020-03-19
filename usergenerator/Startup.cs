using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using Repository;
using Repository.DataContext;
using Repository.Functions;
using usergenerator.Extensions;
using Usergenerator.Filter;

namespace usergenerator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Setting for Log config
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            //Making use of AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //Register services
            services.AddTransient<IUserRepository, UserRepository>();

            //Add cors config which is a service extension
            services.ConfigureCors();

            //Add accessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Add validation filter
            services.AddScoped<ValidationFilterAttribute>();

            //Add logger config which is a service extension
            services.ConfigureLoggerService();
            
            services.AddMvc(config =>
            {
                // Content Negotiation goes here
                config.RespectBrowserAcceptHeader = true;
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problems = new CustomBadRequest(context);

                    return new BadRequestObjectResult(problems);
                };
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2); ;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //Use customised globel exception handler for internal server error
            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
