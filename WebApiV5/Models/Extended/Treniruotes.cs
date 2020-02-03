using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiV5.Models
{
    [MetadataType(typeof(MetaDataT))]
    public partial class Treniruotes
    {
    }

    public class MetaDataT
    {
        [Display(Name = "Laikas")]
        public string Time { get; set; }

        [Display(Name = "Vietų skaičius")]
        public int FreeSpaces { get; set; }

        [Display(Name = "Prisijungusių narių skaičius")]
        public int Joins { get; set; }

        [Display(Name = "Pavadinimas")]
        public string TName { get; set; }
    }
   
}