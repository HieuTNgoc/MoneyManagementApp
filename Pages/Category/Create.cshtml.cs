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
    public class CreateModel : PageModel
    {

        private readonly MoneyManagementV2Context _context;
        private readonly IToastNotification _notify;

        public CreateModel(MoneyManagementV2Context context, IToastNotification notify)
        {
            _context = context;
            _notify = notify;
        }
        public Saver Saver { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
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
            return Page();
        }

        [BindProperty]
        public Cate Cate { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cates.Add(Cate);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                _notify.AddSuccessToastMessage("Add Category successfully.");
            }
            else
            {
                _notify.AddErrorToastMessage("Add Category Failed!");
            }

            return RedirectToPage("./Index");
        }
    }
}
