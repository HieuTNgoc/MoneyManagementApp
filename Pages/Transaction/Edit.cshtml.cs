using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;
using NToastNotify;

namespace MoneyManagementApp.Pages.Transaction
{
    public class EditModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;
        private readonly IToastNotification _notify;

        public EditModel(MoneyManagementV2Context context, IToastNotification notify)
        {
            _context = context;
            _notify = notify;
        }

        [BindProperty]
        public Transction Transction { get; set; } = default!;

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

            var transction =  await _context.Transctions.FirstOrDefaultAsync(m => m.Id == id);
            if (transction == null)
            {
                return NotFound();
            }
            Transction = transction;
            ViewData["AccountId"] = new SelectList(_context.Maccounts, "AccountId", "AccountName");
            var cates = _context.Cates.ToList();
            var new_cates = new List<Cate>();
            foreach (var cate in cates)
            {
                cate.CateName = ((bool)cate.Type ? "Imcome - " : "Cost - ") + cate.CateName;
                new_cates.Add(cate);
            }
            ViewData["CateId"] = new SelectList(new_cates, "CateId", "CateName");
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Transction).State = EntityState.Modified;

            try
            {
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                {
                    _notify.AddSuccessToastMessage("Update Transaction successfully.");
                }
                else
                {
                    _notify.AddErrorToastMessage("Update Transaction Failed!");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransctionExists(Transction.Id))
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

        private bool TransctionExists(int id)
        {
          return _context.Transctions.Any(e => e.Id == id);
        }
    }
}
