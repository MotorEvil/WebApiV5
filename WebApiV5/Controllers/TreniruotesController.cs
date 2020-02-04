using System.Linq;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using WebApiV5.Models;


namespace WebApiV5.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TreniruotesController : Controller
    {
        private DuomenuBazeEntities db = new DuomenuBazeEntities();
        
        // GET: Treniruotes
        [AllowAnonymous]
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

        [Authorize(Roles = "Klientas")]
        public ActionResult Join(int id)
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
            return View("Join");
        }

        [Authorize(Roles = "Klientas")]
        [HttpPost, ActionName("Join")]
        public ActionResult Join([Bind(Exclude = "USerId,Time,FreeSpace,TName")]JoinViewModel model, [Bind(Exclude = "Subscriptions,UserName,Password,FirstName,LastName,ConfirmPassword,BirthDate,PhoneNumber,IsEmailVerified,ActivationCode")]UserJoinViewModel umodel)
        {
            Treniruotes treniruotes = new Treniruotes()
            {
                Id = model.Id,
                UsersString = model.UsersString,
                Joins = model.Joins
            };

            Users user = new Users()
            {
                Id = umodel.Id,
                Email = umodel.Email

            };

            if (ModelState.IsValid)
            {
                treniruotes.Joins++;
                treniruotes.UsersString = treniruotes.UsersString + umodel.Id.ToString() + ",";

                db.Entry(treniruotes).State = EntityState.Modified;
                db.SaveChanges();
            }


            return View("Index");
        }

        [NonAction]
        public void UserJoin(UserJoinViewModel model)
        {
            Users user = new Users()
            {
                Id = model.Id,
                Email = model.Email

            };
        }

    }
    
}
