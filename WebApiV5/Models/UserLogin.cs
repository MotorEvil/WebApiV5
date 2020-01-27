using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApiV5.Models
{
    public class UserLogin
    {
        [Display(Name = "El-pastas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Reikalingas El-pastas")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Reikalingas Slaptazodis")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Prisiminti mano duomenis")]
        public bool RememberMe { get; set; }
    }
}