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

            notificationCounter++;
            messages.Add("Test noti");
            await LoadMessages();
        }
        public async Task LoadMessages()
        {
            await Clients.All.SendAsync("LoadNotification", messages, notificationCounter);
        }
    }
}
