using System;
using System.Collections.Generic;

namespace MoneyManagementApp.Models
{
    public partial class Mesage
    {
        public int Id { get; set; }
        public int? UserId1 { get; set; }
        public int? UserId2 { get; set; }
        public DateTime? Datetime { get; set; }
        public string? Note { get; set; }

        public virtual Saver? UserId1Navigation { get; set; }
        public virtual Saver? UserId2Navigation { get; set; }
    }
}
