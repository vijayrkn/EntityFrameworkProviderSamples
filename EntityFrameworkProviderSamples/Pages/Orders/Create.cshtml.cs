#nullable disable
using System.Diagnostics;
using EntityFrameworkProviderSamples.Data;
using EntityFrameworkProviderSamples.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProviderSamples.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly CosmosDBDataContext _cosmosDBDataContext;
        private readonly MSSQLDataContext _msSQLDataContext;
        private readonly PostgresDataContext _postgresDataContext;
        private readonly SQLLiteDataContext _sqlLiteDataContext;

        public CreateModel(CosmosDBDataContext cosmosDBDataContext,
                           MSSQLDataContext msSQLDataContext,
                           PostgresDataContext postgresDataContext,
                           SQLLiteDataContext sqlLiteDataContext)
        {
            _cosmosDBDataContext = cosmosDBDataContext;
            _msSQLDataContext = msSQLDataContext;
            _postgresDataContext = postgresDataContext;
            _sqlLiteDataContext = sqlLiteDataContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public EntityFramework.Providers Provider { get; set; }

        [BindProperty]
        public Order Order { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            DbContext context = null;

            switch (Provider)
            {
                case EntityFramework.Providers.CosmosDB:
                    _cosmosDBDataContext.Order.Add(Order);
                    context = _cosmosDBDataContext;
                    break;
                case EntityFramework.Providers.MSSQL:
                    _msSQLDataContext.Order.Add(Order); 
                    context = _msSQLDataContext;
                    break;
                case EntityFramework.Providers.PostgreSQL:
                    _postgresDataContext.Order.Add(Order);
                    context= _postgresDataContext;
                    break;
                case EntityFramework.Providers.SQLLite:
                    _sqlLiteDataContext.Order.Add(Order);
                    context=_sqlLiteDataContext;
                    break;
                default:
                    Debug.Assert(false, "Provider not supported in this version of the product.");
                    break;
            }

            await context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
