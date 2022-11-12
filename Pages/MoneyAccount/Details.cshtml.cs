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
    public class DetailsModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public DetailsModel(MoneyManagementV2Context context)
        {
            _context = context;
        }

      public Maccount Maccount { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Maccounts == null)
            {
                return NotFound();
            }

            var maccount = await _context.Maccounts.FirstOrDefaultAsync(m => m.AccountId == id);
            if (maccount == null)
            {
                return NotFound();
            }
            else 
            {
                Maccount = maccount;
            }
            return Page();
        }
    }
}
