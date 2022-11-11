using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.Message
{
    public class DeleteModel : PageModel
    {
        private readonly MoneyManagementApp.Models.MoneyManagementContext _context;

        public DeleteModel(MoneyManagementApp.Models.MoneyManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Mess Mess { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Messes == null)
            {
                return NotFound();
            }

            var mess = await _context.Messes.FirstOrDefaultAsync(m => m.Id == id);

            if (mess == null)
            {
                return NotFound();
            }
            else 
            {
                Mess = mess;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Messes == null)
            {
                return NotFound();
            }
            var mess = await _context.Messes.FindAsync(id);

            if (mess != null)
            {
                Mess = mess;
                _context.Messes.Remove(Mess);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
