using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly MoneyManagementApp.Models.MoneyManagementContext _context;

        public EditModel(MoneyManagementApp.Models.MoneyManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Saver Saver { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Savers == null)
            {
                return NotFound();
            }

            var saver =  await _context.Savers.FirstOrDefaultAsync(m => m.UserId == id);
            if (saver == null)
            {
                return NotFound();
            }
            Saver = saver;
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

            _context.Attach(Saver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaverExists(Saver.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SaverExists(int id)
        {
          return _context.Savers.Any(e => e.UserId == id);
        }
    }
}
