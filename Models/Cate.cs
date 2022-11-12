using System;
using System.Collections.Generic;

namespace MoneyManagementApp.Models
{
    public partial class Cate
    {
        public Cate()
        {
            Transctions = new HashSet<Transction>();
        }

        public int CateId { get; set; }
        public string? CateName { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public bool? Type { get; set; }

        public virtual ICollection<Transction> Transctions { get; set; }
    }
}
