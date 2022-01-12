﻿#nullable disable
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EntityFrameworkProviderSamples.Data
{
    public static class DataContextUtility
    {
        public static async Task EnsureDbCreatedAndSeedAsync<T>(this DbContextOptions<T> options) where T : DbContext
        {
            var builder = new DbContextOptionsBuilder<T>(options);
            builder.UseLoggerFactory(new LoggerFactory());
            builder.ConfigureWarnings(x => x.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning));
            ConstructorInfo c = typeof(T).GetConstructor(new[] { typeof(DbContextOptions<T>) });
            using T context = (T)c.Invoke(new object[] { builder.Options });
            if (await context.Database.EnsureCreatedAsync())
            {
                // Add the code to seed the database
            }
        }
    }
}
