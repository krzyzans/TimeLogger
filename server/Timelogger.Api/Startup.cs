﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Timelogger.Entities;
using Timelogger.Validation;

namespace Timelogger.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        public IConfigurationRoot Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            _environment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            // Add framework services.
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("e-conomic interview"));
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddValidations();

            services.AddMvc(options => options.EnableEndpointRouting = false);

            if (_environment.IsDevelopment())
            {
                services.AddCors();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials());

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMvc();

            var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                SeedDatabase(scope);
            }
        }

        private static void SeedDatabase(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<ApiContext>();
            context.Projects.Add(
                new Project
                {
                    Id = 1,
                    Name = "e-conomic Interview",
                    DeadlineTime = DateTime.Parse("25.02.2024")
                });

            context.Projects.Add(
                new Project
                {
                    Id = 2,
                    Name = "e-conomic Interview",
                    DeadlineTime = DateTime.Parse("26.02.2024")
                });
            context.TimeRegistrations.Add(
                new TimeRegistration()
                {
                    Id = 1,
                    Minutes = 50,
                    ProjectId = 1
                });
            context.TimeRegistrations.Add(
                new TimeRegistration()
                {
                    Id = 2,
                    Minutes = 50,
                    ProjectId = 1
                });

            context.SaveChanges();
        }
    }
}