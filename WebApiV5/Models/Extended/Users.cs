using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApiV5.Models
{
    
    public partial class Users
    {
        public string ConfirmPassword { get; set; }

    }

    public class UserMetaData
    {
        [Display(Name ="Vartotojo vardas")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="Vartotojo vardas būtinas")]
        public string UserName { get; set; }

        [Display(Name ="Vardas")]
        public string FirstName { get; set; }

        [Display(Name = "Pavardė")]
        public string LastName { get; set; }

        [Display(Name = "Slaptažodis")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="Reikalingas slaptažodis")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage ="Slaptažodis negli būti trumpesnis nei 6 simboliai")]
        public string Password { get; set; }

        [Display(Name = "Patvirtinti slaptažodį")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Slaptažodis neatitinka")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Gimimo metai")]
        [DisplayFormat(ApplyFormatInEditMode =true)]
        public string BirthDate { get; set; }

        [Display(Name ="Telefono numeris")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "El-paštas")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="Reikalingas el-paštas")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="Abonimento likutis")]
        public Nullable<int> Subscriptions { get; set; }
    }
}