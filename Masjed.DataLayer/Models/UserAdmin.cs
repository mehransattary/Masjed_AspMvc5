using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masjed.DataLayer.Models
{
    public  class UserAdmin
    {
        [Required]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمزعبور")]
        public string Password { get; set; }

        [Display(Name = "تاییدایمیل")]
        public bool ConfirmEmail { get; set; }

        [Display(Name = "موبایل")]
        public string Phone { get; set; }
    }
}
