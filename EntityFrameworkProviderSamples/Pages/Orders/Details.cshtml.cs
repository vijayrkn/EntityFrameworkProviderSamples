#nullable disable
using System.Diagnostics;
using EntityFrameworkProviderSamples.Data;
using EntityFrameworkProviderSamples.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProviderSamples.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly CosmosDBDataContext _cosmosDBDataContext;
        private readonly MSSQLDataContext _msSQLDataContext;
        private readonly PostgresDataContext _postgresDataContext;
        private readonly SQLLiteDataContext _sqlLiteDataContext;

        public DetailsModel(CosmosDBDataContext cosmosDBDataContext,
                           MSSQLDataContext msSQLDataContext,
                           PostgresDataContext postgresDataContext,
                           SQLLiteDataContext sqlLiteDataContext)
        {
            _cosmosDBDataContext = cosmosDBDataContext;
            _msSQLDataContext = msSQLDataContext;
            _postgresDataContext = postgresDataContext;
            _sqlLiteDataContext = sqlLiteDataContext;
        }

        public Order Order { get; set; }

        public EntityFramework.Providers Provider{ get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id, EntityFramework.Providers? provider)
        {
            if (id == null || provider == null)
            {
                return NotFound();
            }

            Provider = Provider;
            switch (provider)
            {
                case EntityFramework.Providers.CosmosDB:
                    Order = await _cosmosDBDataContext.Order.FirstOrDefaultAsync(m => m.TrackingId == id);
                    break;
                case EntityFramework.Providers.MSSQL:
                    Order = await _msSQLDataContext.Order.FirstOrDefaultAsync(m => m.TrackingId == id);
                    break;
                case EntityFramework.Providers.PostgreSQL:
                    Order = await _postgresDataContext.Order.FirstOrDefaultAsync(m => m.TrackingId == id);
                    break;
                case EntityFramework.Providers.SQLLite:
                    Order = await _sqlLiteDataContext.Order.FirstOrDefaultAsync(m => m.TrackingId == id);
                    break;
                default:
                    Debug.Assert(false, "Provider not supported in this version of the product.");
                    break;
            }

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
