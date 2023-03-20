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
            MesageUserId1Navigations = new HashSet<Mesage>();
            MesageUserId2Navigations = new HashSet<Mesage>();
            Transctions = new HashSet<Transction>();
        }

        public int UserId { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        [StringLength(maximumLength: 255, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 255")]
        public string? Username { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(maximumLength: 255, MinimumLength = 3, ErrorMessage = "Email must be between 3 and 255")]
        public string? Email { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(maximumLength: 255, MinimumLength = 3, ErrorMessage = "Password must be between 3 and 255")]
        public string Password { get; set; } = null!;
        [Display(Name = "Avatar")]
        public string? Avatar { get; set; }

        public virtual ICollection<Maccount> Maccounts { get; set; }
        public virtual ICollection<Mesage> MesageUserId1Navigations { get; set; }
        public virtual ICollection<Mesage> MesageUserId2Navigations { get; set; }
        public virtual ICollection<Transction> Transctions { get; set; }
    }
}
