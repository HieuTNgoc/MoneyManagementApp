using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;
        public IList<Saver> Saver { get; set; } = default!;
        
        public IndexModel(MoneyManagementV2Context context)
        {
            _context = context;
        }

        public Saver curr_account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            string curr_email = HttpContext.Session.GetString("UserName");
            
            curr_account = await _context.Savers.FirstOrDefaultAsync(m => m.Email.Equals(curr_email));
            if (curr_account == null)
            {
                return NotFound();
            }
            //if (_context.Savers != null)
            //{
            //    Saver = await _context.Savers.ToListAsync();
            //    return Page();
            //}
            return Redirect("/User/Details?id=" + curr_account.UserId);
        }

    }
}
