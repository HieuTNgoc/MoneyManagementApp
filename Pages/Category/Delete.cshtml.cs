using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;
using NToastNotify;

namespace MoneyManagementApp.Pages.Category
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
        public Cate Cate { get; set; }
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
            if (id == null || _context.Cates == null)
            {
                return NotFound();
            }

            var cate = await _context.Cates.FirstOrDefaultAsync(m => m.CateId == id);

            if (cate == null)
            {
                return NotFound();
            }
            else
            {
                Cate = cate;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Cates == null)
            {
                return NotFound();
            }
            var cate = await _context.Cates.FindAsync(id);

            if (cate != null)
            {
                Cate = cate;
                _context.Cates.Remove(Cate);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
