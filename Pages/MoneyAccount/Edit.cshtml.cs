using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.MoneyAccount
{
    public class EditModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public EditModel(MoneyManagementV2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Maccount Maccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Maccounts == null)
            {
                return NotFound();
            }

            var maccount =  await _context.Maccounts.FirstOrDefaultAsync(m => m.AccountId == id);
            if (maccount == null)
            {
                return NotFound();
            }
            Maccount = maccount;
           ViewData["UserId"] = new SelectList(_context.Savers, "UserId", "UserId");
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

            _context.Attach(Maccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaccountExists(Maccount.AccountId))
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

        private bool MaccountExists(int id)
        {
          return _context.Maccounts.Any(e => e.AccountId == id);
        }
    }
}
