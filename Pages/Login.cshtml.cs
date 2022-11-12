using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public LoginModel(MoneyManagementV2Context context)
        {
            _context = context;
        }

        [BindProperty]
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
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("UserName", Saver.Email);
                return RedirectToPage("/Index");
            }
        }
    }
}
