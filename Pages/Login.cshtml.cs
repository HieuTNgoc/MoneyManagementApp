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
        public Saver Saver { get; set; } = default!;
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool islogin = _context.Savers.Any(u => u.Email.Equals(Saver.Email) && u.Password.Equals(Saver.Password));


            if (!islogin)
            {
                _notify.AddErrorToastMessage("Wrong Username or Password");
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("UserName", Saver.Email);
                _notify.AddSuccessToastMessage("Login successfully.");
                return RedirectToPage("/Index");
            }
        }
    }
}
