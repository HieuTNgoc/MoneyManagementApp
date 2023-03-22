﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;
using NToastNotify;

namespace MoneyManagementApp.Pages.Transaction
{
    public class DetailsModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;
        private readonly IToastNotification _notify;

        public DetailsModel(MoneyManagementV2Context context, IToastNotification notify)
        {
            _context = context;
            _notify = notify;
        }

        public Transction Transction { get; set; }

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
            if (id == null || _context.Transctions == null)
            {
                return NotFound();
            }

            var transction = await _context.Transctions.FirstOrDefaultAsync(m => m.Id == id);
            if (transction == null)
            {
                return NotFound();
            }
            else 
            {
                Transction = transction;
            }
            return Page();
        }
    }
}
