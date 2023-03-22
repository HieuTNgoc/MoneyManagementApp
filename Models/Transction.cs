using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagementApp.Models
{
    public partial class Transction
    {
        public int Id { get; set; }
        [Display(Name = "User")]
        public int? UserId { get; set; }
        [Display(Name = "Category")]
        public int? CateId { get; set; }
        [Display(Name = "Money Account")]
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
        [BindProperty, DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? Datetime { get; set; }
        [Display(Name = "Note")]
        public string? Note { get; set; }

        public virtual Maccount? Account { get; set; }
        public virtual Cate? Cate { get; set; }
        public virtual Saver? User { get; set; }
    }
}
