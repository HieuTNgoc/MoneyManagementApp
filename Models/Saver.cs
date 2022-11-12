using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagementApp.Models
{
    public partial class Saver
    {
        public Saver()
        {
            Maccounts = new HashSet<Maccount>();
            MessUserId1Navigations = new HashSet<Mess>();
            MessUserId2Navigations = new HashSet<Mess>();
            Transctions = new HashSet<Transction>();
        }

        public int UserId { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string Password { get; set; } = null!;
        public byte[]? Avatar { get; set; }

        public virtual ICollection<Maccount> Maccounts { get; set; }
        public virtual ICollection<Mess> MessUserId1Navigations { get; set; }
        public virtual ICollection<Mess> MessUserId2Navigations { get; set; }
        public virtual ICollection<Transction> Transctions { get; set; }
    }
}
