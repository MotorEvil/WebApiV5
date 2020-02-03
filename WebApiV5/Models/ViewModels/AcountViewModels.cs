using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace WebApiV5.Models.ViewModels
{
    public class UserRegistration
    {
        [Display(Name = "Vartotojo vardas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vartotojo vardas būtinas")]
        public string UserName { get; set; }

        [Display(Name = "Slaptažodis")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Reikalingas slaptažodis")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Slaptažodis negli būti trumpesnis nei 6 simboliai")]
        public string Password { get; set; }

        [Display(Name = "Patvirtinti slaptažodį")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Slaptažodis neatitinka")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "El-paštas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Reikalingas el-paštas")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool IsEmailVerified { get; set; }
        public System.Guid ActivationCode { get; set; }
    }
    public class UserLogin
    {
        [Display(Name = "El-paštas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Reikalingas El-paštas")]
        public string Email { get; set; }

        [Display(Name = "Slaptažodis")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Reikalingas Slaptažodis")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Prisiminti mano duomenis")]
        public bool RememberMe { get; set; }
    }

    public class Crypto
    {
        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value)));
        }
    }
    
   
}