using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.Message
{
    public class EditModel : PageModel
    {
        private readonly MoneyManagementApp.Models.MoneyManagementContext _context;

        public EditModel(MoneyManagementApp.Models.MoneyManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mess Mess { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Messes == null)
            {
                return NotFound();
            }

            var mess =  await _context.Messes.FirstOrDefaultAsync(m => m.Id == id);
            if (mess == null)
            {
                return NotFound();
            }
            Mess = mess;
           ViewData["UserId1"] = new SelectList(_context.Savers, "UserId", "UserId");
           ViewData["UserId2"] = new SelectList(_context.Savers, "UserId", "UserId");
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

            _context.Attach(Mess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessExists(Mess.Id))
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

        private bool MessExists(int id)
        {
          return _context.Messes.Any(e => e.Id == id);
        }
    }
}
