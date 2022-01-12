using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EntityFrameworkProviderSamples.Data;
using Microsoft.Azure.Cosmos;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<CosmosDBDataContext>(options =>
    options.UseCosmos(builder.Configuration.GetConnectionString("CosmosDBDataContext"), databaseName: "OrdersDB",
    options =>
    {
        options.ConnectionMode(ConnectionMode.Direct);
        options.WebProxy(new WebProxy());
    }));

builder.Services.AddDbContext<PostgresDataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDataContext")));

builder.Services.AddDbContext<SQLLiteDataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLLiteDataContext")));

builder.Services.AddDbContext<MSSQLDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLDataContext")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Initialize the Databases

    await using var scope = app.Services?.GetService<IServiceScopeFactory>()?.CreateAsyncScope();

    var sqlLiteOptions = scope?.ServiceProvider.GetRequiredService<DbContextOptions<SQLLiteDataContext>>();
    await DataContextUtility.EnsureDbCreatedAndSeedAsync<SQLLiteDataContext>(sqlLiteOptions);

    var msSQLOptions = scope?.ServiceProvider.GetRequiredService<DbContextOptions<MSSQLDataContext>>();
    await DataContextUtility.EnsureDbCreatedAndSeedAsync<MSSQLDataContext>(msSQLOptions);

    var cosmosDBOptions = scope?.ServiceProvider.GetRequiredService<DbContextOptions<CosmosDBDataContext>>();
    await DataContextUtility.EnsureDbCreatedAndSeedAsync<CosmosDBDataContext>(cosmosDBOptions);

    var postgresOptions = scope?.ServiceProvider.GetRequiredService<DbContextOptions<PostgresDataContext>>();
    await DataContextUtility.EnsureDbCreatedAndSeedAsync<PostgresDataContext>(postgresOptions);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
