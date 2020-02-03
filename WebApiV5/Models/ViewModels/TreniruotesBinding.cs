using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace WebApiV5.Models
{
    public class TreniruotesString : DuomenuBazeEntities
    {
        DuomenuBazeEntities db = new DuomenuBazeEntities();
        
        public int Tid { get; set; }
        public string TString { get; set; }
        public int Join { get; set; }

        public int UserId { get; set; }


        public void Join_Click([Bind(Include = "Tid,TString,Join,UserId")] TreniruotesString treniruotesString)
        {
            Treniruotes treniruote = new Treniruotes()
            {
                Id = treniruotesString.Tid,
                Joins = treniruotesString.Join,
                UsersString = treniruotesString.TString
            };

            treniruote = db.Treniruotes.Where(x => x.Id == treniruotesString.Tid).FirstOrDefault();

            Users user = new Users()
            {
                Id = treniruotesString.UserId
            };

            user = db.Users.Where(x => x.Id == treniruotesString.UserId).FirstOrDefault();

            treniruote.UsersString = treniruote.UsersString + "," + treniruotesString.UserId.ToString();
            treniruote.Joins++;

            db.Entry(treniruote).State = EntityState.Modified;
            db.SaveChanges();
        }

    }

   
}