using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;
using NToastNotify;

namespace MoneyManagementApp.Pages.User
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
        public Saver Saver { get; set; } = default!;
        [BindProperty]
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(maximumLength: 255, MinimumLength = 3, ErrorMessage = "Confirm Password must be between 3 and 255")]
        public string OldPassword { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Savers == null)
            {
                return NotFound();
            }

            var saver = await _context.Savers.FirstOrDefaultAsync(m => m.UserId == id);
            if (saver == null)
            {
                return NotFound();
            }
            Saver = saver;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!OldPassword.Equals(Saver.Password))
            {
                ModelState.AddModelError("OldPassword", "Confirm Password does not correct.");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Saver).State = EntityState.Modified;

            try
            {
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                {
                    _notify.AddSuccessToastMessage("Update account successfully.");
                    HttpContext.Session.SetString("Username", Saver.Username);
                }
                else
                {
                    _notify.AddErrorToastMessage("Update account Failed!");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaverExists(Saver.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Index");
        }

        private bool SaverExists(int id)
        {
            return _context.Savers.Any(e => e.UserId == id);
        }
    }
}
