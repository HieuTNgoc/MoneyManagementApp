using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly MoneyManagementApp.Models.MoneyManagementContext _context;

        public DeleteModel(MoneyManagementApp.Models.MoneyManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Saver Saver { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Savers == null)
            {
                return NotFound();
            }

            var saver = await _context.Savers.FirstOrDefaultAsync(m => m.UserId == id);

            if (saver == null)
            {
                return NotFound();
            }
            else 
            {
                Saver = saver;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Savers == null)
            {
                return NotFound();
            }
            var saver = await _context.Savers.FindAsync(id);

            if (saver != null)
            {
                Saver = saver;
                _context.Savers.Remove(Saver);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
