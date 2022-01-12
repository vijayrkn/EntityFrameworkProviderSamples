#nullable disable
using System.Diagnostics;
using EntityFrameworkProviderSamples.Data;
using EntityFrameworkProviderSamples.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProviderSamples.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly CosmosDBDataContext _cosmosDBDataContext;
        private readonly MSSQLDataContext _msSQLDataContext;
        private readonly PostgresDataContext _postgresDataContext;
        private readonly SQLLiteDataContext _sqlLiteDataContext;

        public IndexModel(CosmosDBDataContext cosmosDBDataContext,
                           MSSQLDataContext msSQLDataContext,
                           PostgresDataContext postgresDataContext,
                           SQLLiteDataContext sqlLiteDataContext)
        {
            _cosmosDBDataContext = cosmosDBDataContext;
            _msSQLDataContext = msSQLDataContext;
            _postgresDataContext = postgresDataContext;
            _sqlLiteDataContext = sqlLiteDataContext;
        }

        [BindProperty]
        public EntityFramework.Providers Provider { get; set; }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            await GetProviderRelevantContentAsync();
        }

        public async Task OnPostAsync()
        {
            await GetProviderRelevantContentAsync();
        }

        private async Task GetProviderRelevantContentAsync()
        {
            switch (Provider)
            {
                case EntityFramework.Providers.CosmosDB:
                    Order = await _cosmosDBDataContext.Order.ToListAsync();
                    break;
                case EntityFramework.Providers.MSSQL:
                    Order = await _msSQLDataContext.Order.ToListAsync();
                    break;
                case EntityFramework.Providers.PostgreSQL:
                    Order = await _postgresDataContext.Order.ToListAsync();
                    break;
                case EntityFramework.Providers.SQLLite:
                    Order = await _sqlLiteDataContext.Order.ToListAsync();
                    break;
                default:
                    Debug.Assert(false, "Provider not supported in this version of the product.");
                    break;
            }
        }
    }
}
