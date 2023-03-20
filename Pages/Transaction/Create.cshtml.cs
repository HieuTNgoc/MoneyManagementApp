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
    public class CreateModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public CreateModel(MoneyManagementV2Context context)
        {
            _context = context;
        }
        [BindProperty]
        public Saver curr_account { get; set; }

        public IList<Cate> Cates { get; set; } = default!;
        public IList<Maccount> Maccounts { get; set; } = default!;
        public async Task<IActionResult> OnGet()
        {
            Maccounts = await _context.Maccounts
                .Include(m => m.User).ToListAsync();
            Cates = await _context.Cates.ToListAsync();
            return Page();
        }

        [BindProperty]
        public Transction Transction { get; set; }
        

        public async Task<IActionResult> OnPostAsync()
        {
            string curr_email = HttpContext.Session.GetString("UserName");
            curr_account = await _context.Savers.FirstOrDefaultAsync(m => m.Email.Equals(curr_email));
            Transction.UserId = curr_account.UserId;

            _context.Transctions.Add(Transction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
