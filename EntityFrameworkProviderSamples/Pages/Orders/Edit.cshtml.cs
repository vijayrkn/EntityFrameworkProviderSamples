#nullable disable
using System.Diagnostics;
using EntityFrameworkProviderSamples.Data;
using EntityFrameworkProviderSamples.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProviderSamples.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly CosmosDBDataContext _cosmosDBDataContext;
        private readonly MSSQLDataContext _msSQLDataContext;
        private readonly PostgresDataContext _postgresDataContext;
        private readonly SQLLiteDataContext _sqlLiteDataContext;

        public EditModel(CosmosDBDataContext cosmosDBDataContext,
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
        public EntityFramework.Providers? Provider { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id, EntityFramework.Providers? provider)
        {
            if (id == null || provider == null)
            {
                return NotFound();
            }

            Provider = provider;
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
                case EntityFramework.Providers.SQLite:
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            DbContext dbContext = null;
            switch (Provider)
            {
                case EntityFramework.Providers.CosmosDB:
                    dbContext = _cosmosDBDataContext;
                    break;
                case EntityFramework.Providers.MSSQL:
                    dbContext = _msSQLDataContext;
                    break;
                case EntityFramework.Providers.PostgreSQL:
                    dbContext = _postgresDataContext;
                    break;
                case EntityFramework.Providers.SQLite:
                    dbContext = _sqlLiteDataContext;
                    break;
                default:
                    Debug.Assert(false, "Provider not supported in this version of the product.");
                    break;
            }

            dbContext.Attach(Order).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("./Index", new { Provider = Provider });
        }
    }
}
