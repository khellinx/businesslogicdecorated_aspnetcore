using System;
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
using Digipolis.BusinessLogicDecorated.Configuration;
using Digipolis.BusinessLogicDecorated.Operators;
using Digipolis.BusinessLogicDecorated.SampleApi.Logic;
using Digipolis.BusinessLogicDecorated.SampleApi.Logic.Inputs;

namespace Digipolis.BusinessLogicDecorated.SampleApi
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

            // Add the business logic
            //OperatorBuilder.ConfigureAsyncGetOperator<Person, GetPersonInput, IAsyncGetOperator<Person, GetPersonInput>>(x => new AsyncAddOperator<Person, GetPersonInput>(x.GetRequiredService<IUowProvider>()))
            //    .AddTransient(services);
            //operationBuilder.ConfigureAsyncGetOperator<Person, GetPersonInput>(x => new AsyncAddOperator<Person, GetPersonInput>(x.GetRequiredService<IUowProvider>()))
            //    .AddTransient(services);

            var operatorBuilder = new OperatorBuilder();
            operatorBuilder.SetDefaultAsyncGetOperatorTypes(typeof(AsyncGetOperator<>), typeof(AsyncGetOperator<,>));

            operatorBuilder.ConfigureAsyncGetOperator<Person, GetPersonInput>()
                .WithPreprocessing()
                .AddTransient(services);
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
                Birthdate = new DateTime(1986, 5, 21)
            };
            var maria = new Person()
            {
                Name = "Maria",
                Birthdate = new DateTime(1983, 7, 14)
            };
            // John and Maria are a couple, how cute! :-)
            john.Partner = maria;
            maria.Partner = john;

            var peter = new Person()
            {
                Name = "Peter",
                Birthdate = new DateTime(1953, 1, 9)
            };

            context.People.Add(john);
            context.People.Add(maria);
            context.People.Add(peter);

            context.SaveChanges();
        }
    }
}
