using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;
using NToastNotify;

namespace MoneyManagementApp.Pages.MoneyAccount
{
    public class DeleteModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;
        private readonly IToastNotification _notify;

        public DeleteModel(MoneyManagementV2Context context, IToastNotification notify)
        {
            _context = context;
            _notify = notify;
        }

        [BindProperty]
        public Maccount Maccount { get; set; }
        public Saver Saver { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string currUser = HttpContext.Session.GetString("Username");
            if (currUser == null)
            {
                return Redirect("/Login");
            }
            Saver = await _context.Savers.FirstOrDefaultAsync(m => m.Username.Equals(currUser));
            if (Saver == null)
            {
                return NotFound();
            }

            if (id == null || _context.Maccounts == null)
            {
                return NotFound();
            }

            var maccount = await _context.Maccounts.FirstOrDefaultAsync(m => m.AccountId == id);

            if (maccount == null)
            {
                return NotFound();
            }
            else
            {
                Maccount = maccount;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Maccounts == null)
            {
                return NotFound();
            }
            var maccount = await _context.Maccounts.FindAsync(id);

            if (maccount != null)
            {
                Maccount = maccount;
                _context.Maccounts.Remove(Maccount);
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                {
                    _notify.AddSuccessToastMessage("Remove Money Account successfully.");
                }
                else
                {
                    _notify.AddErrorToastMessage("Remove Money Account Failed!");
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
