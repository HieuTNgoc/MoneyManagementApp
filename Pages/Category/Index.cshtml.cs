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
    public class IndexModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;
        private readonly IToastNotification _notify;

        public IndexModel(MoneyManagementV2Context context, IToastNotification notify)
        {
            _context = context;
            _notify = notify;
        }

        public IList<Cate> Cate { get;set; } = default!;
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
            if (_context.Cates != null)
            {
                Cate = await _context.Cates.ToListAsync();
            }
            return Page();
        }
    }
}
