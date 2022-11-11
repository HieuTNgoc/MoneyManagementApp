using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.Transaction
{
    public class EditModel : PageModel
    {
        private readonly MoneyManagementApp.Models.MoneyManagementContext _context;

        public EditModel(MoneyManagementApp.Models.MoneyManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Transction Transction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Transctions == null)
            {
                return NotFound();
            }

            var transction =  await _context.Transctions.FirstOrDefaultAsync(m => m.Id == id);
            if (transction == null)
            {
                return NotFound();
            }
            Transction = transction;
           ViewData["AccountId"] = new SelectList(_context.Maccounts, "AccountId", "AccountId");
           ViewData["CateId"] = new SelectList(_context.Cates, "CateId", "CateId");
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

            _context.Attach(Transction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransctionExists(Transction.Id))
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

        private bool TransctionExists(int id)
        {
          return _context.Transctions.Any(e => e.Id == id);
        }
    }
}
