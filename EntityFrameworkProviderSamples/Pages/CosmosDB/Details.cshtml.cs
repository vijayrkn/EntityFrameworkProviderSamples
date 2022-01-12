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

namespace EntityFrameworkProviderSamples.Pages.CosmosDB
{
    public class DetailsModel : PageModel
    {
        private readonly EntityFrameworkProviderSamples.Data.CosmosDBDataContext _context;

        public DetailsModel(EntityFrameworkProviderSamples.Data.CosmosDBDataContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Order.FirstOrDefaultAsync(m => m.TrackingId == id);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
