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
    public class CreateModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;
        private readonly IToastNotification _notify;

        public CreateModel(MoneyManagementV2Context context, IToastNotification notify)
        {
            _context = context;
            _notify = notify;
        }
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

            ViewData["AccountId"] = new SelectList(_context.Maccounts, "AccountId", "AccountName");
            var cates = _context.Cates.ToList();
            var new_cates = new List<Cate>();
            foreach(var cate in cates)
            {
                cate.CateName =  ((bool)cate.Type ? "Imcome - " : "Cost - ") + cate.CateName;
                new_cates.Add(cate);
            }
            ViewData["CateId"] = new SelectList(new_cates, "CateId", "CateName");
            return Page();
        }

        [BindProperty]
        public Transction Transactione { get; set; }
        

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var cate = await _context.Cates.FirstOrDefaultAsync(m => m.CateId.Equals(Transactione.CateId));
            Transactione.Type = cate.Type;
            _context.Transctions.Add(Transactione);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                _notify.AddSuccessToastMessage("Add Transaction successfully.");
            }
            else
            {
                _notify.AddErrorToastMessage("Add Transaction Failed!");
            }

            return RedirectToPage("./Index");
        }
    }
}
