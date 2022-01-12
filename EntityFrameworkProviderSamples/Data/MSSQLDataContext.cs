#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkProviderSamples.Models;

namespace EntityFrameworkProviderSamples.Data
{
    public class MSSQLDataContext : DbContext
    {
        public MSSQLDataContext (DbContextOptions<MSSQLDataContext> options)
            : base(options)
        {
        }

        public DbSet<EntityFrameworkProviderSamples.Models.Order> Order { get; set; }
    }
}
