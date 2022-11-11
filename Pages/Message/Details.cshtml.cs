using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.Message
{
    public class DetailsModel : PageModel
    {
        private readonly MoneyManagementApp.Models.MoneyManagementContext _context;

        public DetailsModel(MoneyManagementApp.Models.MoneyManagementContext context)
        {
            _context = context;
        }

      public Mess Mess { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Messes == null)
            {
                return NotFound();
            }

            var mess = await _context.Messes.FirstOrDefaultAsync(m => m.Id == id);
            if (mess == null)
            {
                return NotFound();
            }
            else 
            {
                Mess = mess;
            }
            return Page();
        }
    }
}
