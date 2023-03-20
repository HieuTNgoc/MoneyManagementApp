using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyManagementApp.Models;
using NToastNotify;

namespace MoneyManagementApp.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;
        private readonly IToastNotification _notify;

        public RegisterModel(MoneyManagementV2Context context, IToastNotification notify)
        {
            _context = context;
            _notify = notify;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Saver Saver { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var oldUsername = _context.Savers.FirstOrDefault(u => u.Username.Equals(Saver.Username));
            var oldEmail = _context.Savers.FirstOrDefault(u => u.Email.Equals(Saver.Email));
            if (oldUsername != null)
            {
                ModelState.AddModelError("Saver.Username", "Username alredy exist, try other one!");
            }
            if (oldEmail != null)
            {
                ModelState.AddModelError("Saver.Email", "Email alredy exist, try other one!");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Savers.Add(Saver);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                _notify.AddSuccessToastMessage("Register successfully.");
            }
            else
            {
                _notify.AddErrorToastMessage("Register Failed!");
            }
            return RedirectToPage("/Index");
        }
    }
}
