using System;
using System.Collections.Generic;

namespace MoneyManagementApp.Models
{
    public partial class Maccount
    {
        public Maccount()
        {
            Transctions = new HashSet<Transction>();
        }

        public int AccountId { get; set; }
        public string? AccountName { get; set; }
        public decimal? Money { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public int? UserId { get; set; }

        public virtual Saver? User { get; set; }
        public virtual ICollection<Transction> Transctions { get; set; }
    }
}
