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
        public Saver curr_account { get; set; } = default!;
        public IList<Cate> Cates { get; set; } = default!;
        public IList<Maccount> Maccounts { get; set; } = default!;
        public IList<Transction> Transctions { get; set; } = default!;
        public decimal total = 0, cost = 0, income = 0;
    
        public async Task<IActionResult> OnGetAsync()
        {
            string curr_email = HttpContext.Session.GetString("UserName");
            if (curr_email == null)
            {
                return Redirect("/Login");
            }
            curr_account = await _context.Savers.FirstOrDefaultAsync(m => m.Email.Equals(curr_email));
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

            if (curr_account == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}