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
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Category Name must be between 3 and 50")]
        public string? CateName { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public bool? Type { get; set; }

        public virtual ICollection<Transction> Transctions { get; set; }
    }
}
