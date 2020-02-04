using System;
using System.ComponentModel.DataAnnotations;


namespace WebApiV5.Models.ViewModels
{
    public class CreateViewModel
    {
        [Display(Name = "Vartotojo vardas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vartotojo vardas būtinas")]
        public string UserName { get; set; }

        [Display(Name = "Vardas")]
        public string FirstName { get; set; }

        [Display(Name = "Pavardė")]
        public string LastName { get; set; }

        [Display(Name = "Slaptažodis")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Reikalingas slaptažodis")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Slaptažodis negli būti trumpesnis nei 6 simboliai")]
        public string Password { get; set; }

        [Display(Name = "Patvirtinti slaptažodį")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Slaptažodis neatitinka")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Gimimo metai")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public string BirthDate { get; set; }

        [Display(Name = "Telefono numeris")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "El-paštas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Reikalingas el-paštas")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Abonimento likutis")]
        public Nullable<int> Subscriptions { get; set; }

        [Display(Name = "Ar el-paštas patvirtintas?")]
        public bool IsEmailVerified { get; set; }
        public System.Guid ActivationCode { get; set; }

        public string Role { get; set; } = "Klientas";
    }

    public class EditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Vartotojo vardas")]
        public string UserName { get; set; }

        [Display(Name = "Vardas")]
        public string FirstName { get; set; }

        [Display(Name = "Pavardė")]
        public string LastName { get; set; }

        [Display(Name = "Slaptažodis")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Slaptažodis negli būti trumpesnis nei 6 simboliai")]
        public string Password { get; set; }

        [Display(Name = "Patvirtinti slaptažodį")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Slaptažodis neatitinka")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Gimimo metai")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public string BirthDate { get; set; }

        [Display(Name = "Telefono numeris")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "El-paštas")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Abonimento likutis")]
        public Nullable<int> Subscriptions { get; set; }

        [Display(Name = "Ar el-paštas patvirtintas?")]
        public bool IsEmailVerified { get; set; }
        public System.Guid ActivationCode { get; set; }

        [Display(Name = "Rolė")]
        public string Role { get; set; } = "Klientas";
    }
}