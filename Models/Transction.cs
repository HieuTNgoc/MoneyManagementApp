using System;
using System.Collections.Generic;

namespace MoneyManagementApp.Models
{
    public partial class Transction
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? CateId { get; set; }
        public int? AccountId { get; set; }
        public decimal? Money { get; set; }
        public bool? Type { get; set; }
        public DateTime? Datetime { get; set; }
        public string? Note { get; set; }

        public virtual Maccount? Account { get; set; }
        public virtual Cate? Cate { get; set; }
        public virtual Saver? User { get; set; }
    }
}
