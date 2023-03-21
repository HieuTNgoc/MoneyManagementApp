using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;

namespace MoneyManagementApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MoneyManagementV2Context _context;

        public IndexModel(MoneyManagementV2Context context)
        {
            _context = context;
        }
        public Saver Saver { get; set; } = default!;
        public Saver curr_account { get; set; } = default!;
        public IList<Cate> Cates { get; set; } = default!;
        public IList<Maccount> Maccounts { get; set; } = default!;
        public IList<Transction> Transctions { get; set; } = default!;
        public decimal total = 0, cost = 0, income = 0;
    
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

            Maccounts = await _context.Maccounts.ToListAsync();
            Cates = await _context.Cates.ToListAsync();
            Transctions = await _context.Transctions.ToListAsync();

            foreach(var acc in Maccounts)
            {
                total = total + (decimal) acc.Money;
            }

            foreach(var tran in Transctions)
            {
                if (tran.Type == false)
                {
                    cost = cost + (decimal) tran.Money;
                }
                else
                {
                    income = income + (decimal) tran.Money;
                }
            }

            total = total - cost + income;

            
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToPage("/Login");
        }
    }
}