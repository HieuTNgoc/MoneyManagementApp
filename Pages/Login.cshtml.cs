using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;
using NToastNotify;
using System.Security.Principal;

namespace MoneyManagementApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;
        private readonly IToastNotification _notify;

        public LoginModel(MoneyManagementV2Context context, IToastNotification notify)
        {
            _context = context;
            _notify = notify;
        }

        [BindProperty]
        public Saver SaverLogin { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var saver = _context.Savers.FirstOrDefault(u => u.Email.Equals(SaverLogin.Email) && u.Password.Equals(SaverLogin.Password));
            if (saver != null)
            {
                HttpContext.Session.SetString("Username", saver.Username);
                _notify.AddSuccessToastMessage("Login successfully.");
                return RedirectToPage("/Index");
            }
            else
            {
                _notify.AddErrorToastMessage("Wrong Email or Password");
                return Page();
            }
        }
    }
}
