using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.Category
{
    public class EditModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public EditModel(MoneyManagementV2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Cate Cate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Cates == null)
            {
                return NotFound();
            }

            var cate =  await _context.Cates.FirstOrDefaultAsync(m => m.CateId == id);
            if (cate == null)
            {
                return NotFound();
            }
            Cate = cate;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Cate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CateExists(Cate.CateId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CateExists(int id)
        {
          return _context.Cates.Any(e => e.CateId == id);
        }
    }
}
