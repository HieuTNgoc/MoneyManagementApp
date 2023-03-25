using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MoneyManagementApp.Models;
using NToastNotify;

namespace MoneyManagementApp.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly MoneyManagementV2Context _context;

        public NotificationHub(MoneyManagementV2Context context)
        {
            _context = context;
        }
        public static int notificationCounter = 0;
        public static List<string> messages = new();
        public static List<int> accountIds = new();


        public async Task SendMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                notificationCounter++;
                messages.Add(message);
                await LoadMessages();
            }
        }

        public IList<Maccount> Maccount { get; set; } = default!;
        public async Task SendNotification(string userId)
        {
            int id = int.Parse(userId);
            Maccount = await _context.Maccounts.Where(t => t.UserId == id).ToListAsync();
            notificationCounter = 0;
            messages = new();
            accountIds = new();

            foreach (var m in Maccount) { 
                if (m.Money < 10000)
                {
                    notificationCounter++;
                    messages.Add("Tài khoản " + m.AccountName + " còn dưới $10,000 hãy bổ xung ngay.");
                    accountIds.Add(m.AccountId);
                }
            }
            await LoadMessages();
        }
        public async Task LoadMessages()
        {
            await Clients.All.SendAsync("LoadNotification", messages, accountIds, notificationCounter);
        }
    }
}
