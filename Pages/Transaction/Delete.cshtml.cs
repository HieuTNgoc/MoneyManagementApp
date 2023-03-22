using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;
using NToastNotify;

namespace MoneyManagementApp.Pages.Transaction
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
      public Transction Transction { get; set; }

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

            if (id == null || _context.Transctions == null)
            {
                return NotFound();
            }

            var transction = await _context.Transctions.FirstOrDefaultAsync(m => m.Id == id);

            if (transction == null)
            {
                return NotFound();
            }
            else 
            {
                Transction = transction;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Transctions == null)
            {
                return NotFound();
            }
            var transction = await _context.Transctions.FindAsync(id);

            if (transction != null)
            {
                Transction = transction;
                _context.Transctions.Remove(Transction);
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                {
                    _notify.AddSuccessToastMessage("Remove Transaction successfully.");
                }
                else
                {
                    _notify.AddErrorToastMessage("Remove Transaction Failed!");
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
