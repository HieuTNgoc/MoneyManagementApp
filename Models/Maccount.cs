using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagementApp.Models
{
    public partial class Maccount
    {
        public Maccount()
        {
            Transctions = new HashSet<Transction>();
        }

        public int AccountId { get; set; }
        [Display(Name = "Account Name")]
        [Required(ErrorMessage = "Account Name is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Account Name must be between 3 and 50")]
        public string? AccountName { get; set; }
        [Display(Name = "Total Money")]
        [Required(ErrorMessage = "Total Money is required")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? Money { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public int? UserId { get; set; }

        public virtual Saver? User { get; set; }
        public virtual ICollection<Transction> Transctions { get; set; }
    }
}
