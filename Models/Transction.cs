using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagementApp.Models
{
    public partial class Transction
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? CateId { get; set; }
        public int? AccountId { get; set; }

        [Display(Name = "Money")]
        [Required(ErrorMessage = "Money is required")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? Money { get; set; }
        public bool? Type { get; set; }
        [Display(Name = "Datetime")]
        [Required(ErrorMessage = "Datetime is required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? Datetime { get; set; }
        [Display(Name = "Note")]
        public string? Note { get; set; }

        public virtual Maccount? Account { get; set; }
        public virtual Cate? Cate { get; set; }
        public virtual Saver? User { get; set; }
    }
}
