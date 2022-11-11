using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.MoneyAccount
{
    public class CreateModel : PageModel
    {
        private readonly MoneyManagementApp.Models.MoneyManagementContext _context;

        public CreateModel(MoneyManagementApp.Models.MoneyManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.Savers, "UserId", "UserId");
            return Page();
        }

        [BindProperty]
        public Maccount Maccount { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Maccounts.Add(Maccount);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
