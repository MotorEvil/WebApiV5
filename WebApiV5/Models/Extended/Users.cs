using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApiV5.Models
{
    [MetadataType(typeof(MetaData))]
    public partial class Users
    {
        public string ConfirmPassword { get; set; }

    }
     
    public class MetaData
    {
        [Display(Name = "Vartotojo vardas")]
        public string UserName { get; set; }

        [Display(Name = "Vardas")]
        public string FirstName { get; set; }

        [Display(Name = "Pavardė")]
        public string LastName { get; set; }

        [Display(Name = "Slaptažodis")]
        public string Password { get; set; }

        [Display(Name = "Patvirtinti slaptažodį")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Gimimo metai")]
        public string BirthDate { get; set; }

        [Display(Name = "Telefono numeris")]
        public string PhoneNumber { get; set; }

        [Display(Name = "El-paštas")]
        public string Email { get; set; }

        [Display(Name = "Abonimento likutis")]
        public Nullable<int> Subscriptions { get; set; }

    }
   
}