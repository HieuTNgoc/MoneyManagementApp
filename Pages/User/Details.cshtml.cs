﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.User
{
    public class DetailsModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public DetailsModel(MoneyManagementV2Context context)
        {
            _context = context;
        }

      public Saver Saver { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Savers == null)
            {
                return NotFound();
            }

            var saver = await _context.Savers.FirstOrDefaultAsync(m => m.UserId == id);
            if (saver == null)
            {
                return NotFound();
            }
            else 
            {
                Saver = saver;
            }
            return Page();
        }
    }
}
