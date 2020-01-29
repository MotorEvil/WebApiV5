using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Windows;

namespace WebApiV5.Models
{
    public class TreniruotesBinding : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        DuomenuBazeEntities db = new DuomenuBazeEntities();

        private string Button_Click(string value)
        {

            Treniruotes treniruotes = new Treniruotes();
            value = "Join";

            if (treniruotes.FreeSpaces != treniruotes.Joins)
            {
                if (value == "Join")
                {
                    treniruotes.Joins = +1;
                    value = "Out";
                    return value;
                }
                if (value == "Out")
                {
                    treniruotes.Joins = -1;
                    value = "Join";
                    return value;
                }
            }
            else
            {
                value = "Full";
                return value;
            }

            return value;
        }
    }



    public class TreniruotesCreate
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public int FreeSpace { get; set; }
        public int Joins { get; set; }
        public string TName { get; set; }


    }

   
}