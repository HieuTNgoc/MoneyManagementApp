using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.MoneyAccount
{
    public class IndexModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public IndexModel(MoneyManagementV2Context context)
        {
            _context = context;
        }

        public IList<Maccount> Maccount { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Maccounts != null)
            {
                Maccount = await _context.Maccounts
                .Include(m => m.User).ToListAsync();
            }
        }
    }
}
