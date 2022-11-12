using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public RegisterModel(MoneyManagementV2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Saver Saver { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Savers.Add(Saver);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
