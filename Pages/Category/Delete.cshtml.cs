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
    public class DeleteModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public DeleteModel(MoneyManagementV2Context context)
        {
            _context = context;
        }

        [BindProperty]
      public Cate Cate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Cates == null)
            {
                return NotFound();
            }
            var cate = await _context.Cates.FindAsync(id);

            if (cate != null)
            {
                Cate = cate;
                _context.Cates.Remove(Cate);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
