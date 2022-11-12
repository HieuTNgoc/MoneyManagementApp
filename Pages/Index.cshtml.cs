using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MoneyManagementContext _context;

        public IndexModel(MoneyManagementContext context)
        {
            _context = context;
        }
        public Saver curr_account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            string curr_email = HttpContext.Session.GetString("UserName");
            if (curr_email == null)
            {
                return Redirect("/Login");
            }
            curr_account = await _context.Savers.FirstOrDefaultAsync(m => m.Email.Equals(curr_email));
            if (curr_account == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}