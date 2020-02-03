using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using System.Data;
using System.Net.Mail;
using System.Web.Helpers;
using WebApiV5.Models.ViewModels;
using WebApiV5.Models;


namespace WebApiV5.Controllers
{
    
    public class TreniruotesController : Controller
    {
        private DuomenuBazeEntities db = new DuomenuBazeEntities();
        
        // GET: Treniruotes
        public ActionResult Index()
        {
            return View(db.Treniruotes.ToList());
        }

        // GET: Treniruotes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
        // GET: Treniruotes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Treniruotes/Create
        [HttpPost]
        public ActionResult Create(Treniruotes treniruotes)
        {
            if (ModelState.IsValid)
            {
                db.Treniruotes.Add(treniruotes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(treniruotes);

        }

        // GET: Treniruotes/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treniruotes treniruotes = db.Treniruotes.Find(id);
            if (treniruotes == null)
            {
                return HttpNotFound();
            }
            return View(treniruotes);
        }

        // POST: Treniruotes/Edit/5
        [HttpPost]
        public ActionResult Edit(Treniruotes treniruotes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treniruotes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(treniruotes);
        }

        
        // GET: Treniruotes/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treniruotes treniruotes = db.Treniruotes.Find(id);
            if (treniruotes == null)
            {
                return HttpNotFound();
            }
            return View(treniruotes);
        }

        // POST: Treniruotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Treniruotes treniruotes = db.Treniruotes.Find(id);
            db.Treniruotes.Remove(treniruotes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




        [Authorize]
        [HttpPost, ActionName("Join")]
        public ActionResult Join(TreniruotesString model)
        {

            if (ModelState.IsValid)
            {
                using(DuomenuBazeEntities db = new DuomenuBazeEntities()) 
                { 
                    var user = db.Users.Where(a => a.Id == model.UserId).FirstOrDefault();
                    if (user != null)
                    {

                        Treniruotes treniruotes = db.Treniruotes.FirstOrDefault();
                        //Treniruotes treniruotes = new Treniruotes();
                        treniruotes.UsersString += "," + user.Id.ToString();
                        treniruotes.Joins = treniruotes.Joins + 1;

                        db.Treniruotes.Add(treniruotes);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                
            }
            return View("Index");
        }
    }
}
