using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;
using NToastNotify;

namespace MoneyManagementApp.Pages.Category
{
    public class EditModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;
        private readonly IToastNotification _notify;

        public EditModel(MoneyManagementV2Context context, IToastNotification notify)
        {
            _context = context;
            _notify = notify;
        }

        [BindProperty]
        public Cate Cate { get; set; } = default!;
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

            var cate =  await _context.Cates.FirstOrDefaultAsync(m => m.CateId == id);
            if (cate == null)
            {
                return NotFound();
            }
            Cate = cate;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Cate).State = EntityState.Modified;

            try
            {
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                {
                    _notify.AddSuccessToastMessage("Update Category successfully.");
                }
                else
                {
                    _notify.AddErrorToastMessage("Update Category Failed!");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CateExists(Cate.CateId))
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

        private bool CateExists(int id)
        {
          return _context.Cates.Any(e => e.CateId == id);
        }
    }
}
