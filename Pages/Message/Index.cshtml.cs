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
    public class IndexModel : PageModel
    {
        private readonly MoneyManagementApp.Models.MoneyManagementContext _context;

        public IndexModel(MoneyManagementApp.Models.MoneyManagementContext context)
        {
            _context = context;
        }

        public IList<Mess> Mess { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Messes != null)
            {
                Mess = await _context.Messes
                .Include(m => m.UserId1Navigation)
                .Include(m => m.UserId2Navigation).ToListAsync();
            }
        }
    }
}
