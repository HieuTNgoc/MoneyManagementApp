using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyManagementApp.Models;
using System;

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
        public IList<Cate> Cates { get; set; } = default!;
        public IList<Maccount> Maccounts { get; set; } = default!;
        public IList<Transction> Transctions { get; set; } = default!;
        public int total = 0, cost = 0, income = 0;

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

            Maccounts = await _context.Maccounts.Where(t => t.UserId == Saver.UserId).ToListAsync();
            Cates = await _context.Cates.ToListAsync();
            Transctions = await _context.Transctions.Where(t => t.UserId == Saver.UserId).ToListAsync();

            foreach (var acc in Maccounts)
            {
                foreach (var tran in Transctions)
                {
                    if (acc.AccountId == tran.AccountId)
                    {

                        if (tran.Type == false)
                        {
                            cost = cost + (int)tran.Money;
                            acc.Money += tran.Money;
                        }
                        else
                        {
                            income = income + (int)tran.Money;
                            acc.Money -= tran.Money;
                        }
                    }
                }
                total = total + (int)acc.Money;
            }

            //foreach (var tran in Transctions)
            //{
            //    if (tran.Type == false)
            //    {
            //        cost = cost + (int)tran.Money;
            //    }
            //    else
            //    {
            //        income = income + (int)tran.Money;
            //    }
            //}

            total = total - cost + income;
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToPage("/Login");
        }

        public async Task<IActionResult> OnGetFilter(int AccountId)
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
            Cates = await _context.Cates.ToListAsync();
            var trans = from m in _context.Transctions select m;
            if (AccountId != 0)
            {
                trans = trans.Where(m => m.AccountId == AccountId);
            }
            Transctions = await trans.Where(t => t.UserId == Saver.UserId).OrderByDescending(t => t.Datetime).ToListAsync();

            List<Transction> SortedList = Transctions.OrderBy(t => t.Datetime).ToList();
            SortedList.Sort(delegate (Transction tr1, Transction tr2) { return DateTime.Compare((DateTime)tr1.Datetime, (DateTime)tr2.Datetime); });

            Transctions = SortedList;

            var data = new List<ChartObj>();
            int count_income = 0;
            int count_cost = 0;
            foreach (var acc in Cates)
            {
                var item = new ChartObj() { x = DateTime.Now, y = 0, name = "", type = false };
                foreach (var tran in Transctions)
                {
                    if (acc.CateId == tran.CateId)
                    {
                        item.y += (int)tran.Money;
                        item.x = (DateTime)tran.Datetime;
                    }
                }
                if (item.y != 0)
                {
                    item.name = acc.CateName;
                    item.type = (bool)acc.Type;
                    data.Add(item);
                    if (item.type == false)
                    {
                        count_cost++;
                    }
                    else
                    {
                        count_income++;
                    }
                }
            }
            if (count_cost == 0)
            {
                data.Add(new ChartObj() { x = DateTime.Now, y = 1, name = "Empty", type = false });
            }
            if (count_income == 0)
            {
                data.Add(new ChartObj() { x = DateTime.Now, y = 1, name = "Empty", type = true });
            }
            return new JsonResult(data);
        }
    }

    public class ChartObj
    {
        public DateTime x { get; set; }
        public int y { get; set; }
        public string name { get; set; }
        public bool type { get; set; }
    }
}