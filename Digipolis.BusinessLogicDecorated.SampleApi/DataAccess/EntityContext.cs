using Digipolis.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Digipolis.BusinessLogicDecorated.SampleApi.Entities;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Digipolis.BusinessLogicDecorated.SampleApi.DataAccess
{
    public class EntityContext : EntityContextBase<EntityContext>
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Home> Homes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Disable cascading deletes
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
