using System;
using System.Collections.Generic;

namespace MoneyManagementApp.Models
{
    public partial class Saver
    {
        public Saver()
        {
            Maccounts = new HashSet<Maccount>();
            MesageUserId1Navigations = new HashSet<Mesage>();
            MesageUserId2Navigations = new HashSet<Mesage>();
            Transctions = new HashSet<Transction>();
        }

        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public string? Avatar { get; set; }

        public virtual ICollection<Maccount> Maccounts { get; set; }
        public virtual ICollection<Mesage> MesageUserId1Navigations { get; set; }
        public virtual ICollection<Mesage> MesageUserId2Navigations { get; set; }
        public virtual ICollection<Transction> Transctions { get; set; }
    }
}
