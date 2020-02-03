/*using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApiV5.Models;
using WebMatrix.WebData;

namespace WebApiV5.Controllers
{
    public class JoinController : Controller
    {
        private DuomenuBazeEntities db = new DuomenuBazeEntities();

        [Authorize]
        [HttpPost, ActionName("Join")]
        public ActionResult Join(int id, Treniruotes treniruotes)
        {
                using (DuomenuBazeEntities db = new DuomenuBazeEntities())
                {
                     Users uid = new Users(); 
                    //var userid = WebSecurity.GetUserId(User.Identity.Name);
                    var user = db.Users.Where(a => a.Id == uid.Id).FirstOrDefault();
                    if (user != null)
                    {
                   treniruotes = db.Treniruotes.Find(id);


                    // Treniruotes treniruotes = db.Treniruotes.FirstOrDefault();
                    //Treniruotes treniruotes = new Treniruotes();
                    treniruotes.UsersString += "," + user.Id.ToString();
                        treniruotes.Joins = treniruotes.Joins + 1;

                        db.Entry(treniruotes).State = EntityState.Modified;
                        // db.Treniruotes.Add(treniruotes);
                        db.SaveChanges();
                    }
                }
            
           
            return new EmptyResult();
        }
    }
}*/