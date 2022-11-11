using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.Transaction
{
    public class DetailsModel : PageModel
    {
        private readonly MoneyManagementApp.Models.MoneyManagementContext _context;

        public DetailsModel(MoneyManagementApp.Models.MoneyManagementContext context)
        {
            _context = context;
        }

      public Transction Transction { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Transctions == null)
            {
                return NotFound();
            }

            var transction = await _context.Transctions.FirstOrDefaultAsync(m => m.Id == id);
            if (transction == null)
            {
                return NotFound();
            }
            else 
            {
                Transction = transction;
            }
            return Page();
        }
    }
}
