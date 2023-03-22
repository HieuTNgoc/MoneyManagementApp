using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.Category
{
    public class DetailsModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public DetailsModel(MoneyManagementV2Context context)
        {
            _context = context;
        }

        public Cate Cate { get; set; }

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

            var cate = await _context.Cates.FirstOrDefaultAsync(m => m.CateId == id);
            if (cate == null)
            {
                return NotFound();
            }
            else
            {
                Cate = cate;
            }
            return Page();
        }
    }
}
