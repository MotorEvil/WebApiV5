using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiV5.Models
{
    public class TreniruotesCreate
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public int FreeSpace { get; set; }
        public int Joins { get; set; }
        public string TName { get; set; }


    }
}