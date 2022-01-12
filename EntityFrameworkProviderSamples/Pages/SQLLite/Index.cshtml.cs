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

namespace EntityFrameworkProviderSamples.Pages.SQLLite
{
    public class IndexModel : PageModel
    {
        private readonly EntityFrameworkProviderSamples.Data.SQLLiteDataContext _context;

        public IndexModel(EntityFrameworkProviderSamples.Data.SQLLiteDataContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Order.ToListAsync();
        }
    }
}
