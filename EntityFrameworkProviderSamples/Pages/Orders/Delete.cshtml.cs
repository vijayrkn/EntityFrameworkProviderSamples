#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkProviderSamples.Data;
using EntityFrameworkProviderSamples.Models;
using System.Diagnostics;

namespace EntityFrameworkProviderSamples.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly CosmosDBDataContext _cosmosDBDataContext;
        private readonly MSSQLDataContext _msSQLDataContext;
        private readonly PostgresDataContext _postgresDataContext;
        private readonly SQLLiteDataContext _sqlLiteDataContext;

        public DeleteModel(CosmosDBDataContext cosmosDBDataContext,
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
        public Order Order { get; set; }

        [BindProperty]
        public EntityFramework.Providers Provider { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id, EntityFramework.Providers? provider)
        {
            if (id == null)
            {
                return NotFound();
            }

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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DbContext dbContext = null;
            switch (Provider)
            {
                case EntityFramework.Providers.CosmosDB:
                    dbContext = _cosmosDBDataContext;
                    Order = await _cosmosDBDataContext.Order.FindAsync(id);
                    if (Order != null)
                    {
                        _cosmosDBDataContext.Order.Remove(Order);
                    }
                    break;
                case EntityFramework.Providers.MSSQL:
                    dbContext = _msSQLDataContext;
                    Order = await _msSQLDataContext.Order.FindAsync(id);
                    if (Order != null)
                    {
                        _msSQLDataContext.Order.Remove(Order);
                    }
                    break;
                case EntityFramework.Providers.PostgreSQL:
                    dbContext = _postgresDataContext;
                    Order = await _postgresDataContext.Order.FindAsync(id);
                    if (Order != null)
                    {
                        _postgresDataContext.Order.Remove(Order);
                    }
                    break;
                case EntityFramework.Providers.SQLLite:
                    dbContext = _sqlLiteDataContext;
                    Order = await _sqlLiteDataContext.Order.FindAsync(id);
                    if (Order != null)
                    {
                        _sqlLiteDataContext.Order.Remove(Order);
                    }
                    break;
                default:
                    Debug.Assert(false, "Provider not supported in this version of the product.");
                    break;
            }

            await dbContext.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
