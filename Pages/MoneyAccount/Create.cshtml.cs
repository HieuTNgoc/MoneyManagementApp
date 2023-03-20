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
    public class CreateModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public CreateModel(MoneyManagementV2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Maccount Maccount { get; set; }
        [BindProperty]
        public Saver curr_account { get; set; }
        public async Task<IActionResult> OnGet()
        {
            string curr_email = HttpContext.Session.GetString("UserName");
            curr_account = await _context.Savers.FirstOrDefaultAsync(m => m.Email.Equals(curr_email));

            if (curr_account == null)
            {
                return NotFound();
            }
            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            string curr_email = HttpContext.Session.GetString("UserName");
            curr_account = await _context.Savers.FirstOrDefaultAsync(m => m.Email.Equals(curr_email));
            Maccount.UserId = curr_account.UserId;
            
            _context.Maccounts.Add(Maccount);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
