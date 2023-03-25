using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages.Transaction
{
    public class IndexModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public IndexModel(MoneyManagementV2Context context)
        {
            _context = context;
        }

        public IList<Transction> Transction { get;set; } = default!;

        public Saver Saver { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedCategory { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? MinVal { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? MaxVal { get; set; }
        public async Task<IActionResult> OnGetAsync()
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
            if (_context.Transctions != null)
            {
                var tran = from m in _context.Transctions select m;

                if (!string.IsNullOrEmpty(SearchString))
                {
                    tran = tran.Where(p => (p.Note.Contains(SearchString)));
                }

                if (SelectedCategory != 0 && SelectedCategory != null)
                {
                    tran = tran.Where(p => p.CateId == SelectedCategory);
                }
                Transction = await tran
                .Include(t => t.Account)
                .Include(t => t.Cate)
                .Include(t => t.User).ToListAsync();
            }
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
    }
}
