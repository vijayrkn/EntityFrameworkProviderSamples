#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkProviderSamples.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EntityFrameworkProviderSamples.Data
{
    public class CosmosDBDataContext : DbContext
    {
        public CosmosDBDataContext (DbContextOptions<CosmosDBDataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.ConfigureWarnings(x => x.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning));

        public DbSet<EntityFrameworkProviderSamples.Models.Order> Order { get; set; }
    }
}
