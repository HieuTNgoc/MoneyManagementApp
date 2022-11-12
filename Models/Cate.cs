using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public int? Icon { get; set; }

        public int? Color { get; set; }
        public bool? Type { get; set; }

        public virtual ICollection<Transction> Transctions { get; set; }
    }
}
