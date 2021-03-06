﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Digipolis.DataAccess;
using Digipolis.BusinessLogicDecorated.SampleApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using Digipolis.BusinessLogicDecorated.SampleApi.Entities;

namespace Digipolis.BusinessLogicDecorated.SampleApi.Startup
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Add the in memory database and context
            services.AddEntityFrameworkInMemoryDatabase();

            services.AddDataAccess<EntityContext>();
            services.AddDbContext<EntityContext>(builder => builder.UseInMemoryDatabase("BusinessLogicDecorated"));

            // Add the Unit of work scope
            services.AddScoped<IUnitOfWorkScope, UnitOfWorkScope>();

            // Add the business logic
            services.AddBusinessLogic();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            // Seed the database
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EntityContext>();
                SeedContext(context);
            }
        }

        // Seed data into context for test purposes
        public void SeedContext(EntityContext context)
        {
            var john = new Person()
            {
                Name = "John",
                Birthdate = new DateTime(1986, 5, 21),
                Address = "Generaal Armstrongweg 1, 2020 Antwerpen",
            };
            var maria = new Person()
            {
                Name = "Maria",
                Birthdate = new DateTime(1983, 7, 14),
                Address = "Generaal Armstrongweg 1, 2020 Antwerpen",
            };
            // John and Maria are a couple, how cute! :-)
            john.Partner = maria;
            maria.Partner = john;

            var peter = new Person()
            {
                Name = "Peter",
                Birthdate = new DateTime(1953, 1, 9),
                Address = "Grote Markt 1, 2000 Antwerpen",
            };
            var chris = new Person()
            {
                Name = "Chris",
                Birthdate = new DateTime(1971, 3, 27),
                Address = "Grote Markt 1, 2000 Antwerpen",
            };
            var ella = new Person()
            {
                Name = "Ella",
                Birthdate = new DateTime(1990, 11, 30),
                Address = "Nationalestraat 101, 2000 Antwerpen",
            };

            context.People.Add(john);
            context.People.Add(maria);
            context.People.Add(peter);
            context.People.Add(chris);
            context.People.Add(ella);

            var digipolis = new Home()
            {
                Address = "Generaal Armstrongweg 1, 2020 Antwerpen",
                NumberOfRooms = 10
            };

            var stadhuis = new Home()
            {
                Address = "Grote Markt 1, 20à0 Antwerpen",
                NumberOfRooms = 16
            };

            context.Homes.Add(digipolis);
            context.Homes.Add(stadhuis);

            context.SaveChanges();
        }
    }
}
