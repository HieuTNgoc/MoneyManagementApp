using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.Transaction
{
    public class CreateModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public CreateModel(MoneyManagementV2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AccountId"] = new SelectList(_context.Maccounts, "AccountId", "AccountId");
        ViewData["CateId"] = new SelectList(_context.Cates, "CateId", "CateId");
        ViewData["UserId"] = new SelectList(_context.Savers, "UserId", "UserId");
            return Page();
        }

        [BindProperty]
        public Transction Transction { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Transctions.Add(Transction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
