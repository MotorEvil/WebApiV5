using System;
using System.Linq;
using System.Web.Mvc;
using WebApiV5.Models;
using System.Net;
using System.Data.Entity;
using System.Data;
using System.Net.Mail;
using WebApiV5.Models.ViewModels;
using Crypto = WebApiV5.Models.ViewModels.Crypto;

namespace WebApiV5.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private DuomenuBazeEntities db = new DuomenuBazeEntities();
        // GET: Users
        public ActionResult UsersList()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            if (id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users==null)
            {
                return HttpNotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "IsEmailVerified, ActivationCode")] CreateViewModel model)
        {
            var user = new Users()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Email = model.Email,
                BirthDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber,
                ConfirmPassword = model.ConfirmPassword,
                ActivationCode = model.ActivationCode,
                IsEmailVerified = model.IsEmailVerified,
                Subscriptions = model.Subscriptions,
                Role = model.Role
            };
            bool Status = false;
            string message = "";

            //Model Validation
            if (ModelState.IsValid)
            {
                #region Email is already exist
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email alrerady exist");
                    return View(model);
                }
                #endregion

                #region Generate Activation Code
                user.ActivationCode = Guid.NewGuid();
                #endregion

                #region Password hashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                #endregion

                user.IsEmailVerified = false;

                #region Save to DataBAse
                    db.Users.Add(user);
                    db.SaveChanges();

                    //Send email to user
                    SendeVertificationEmail(user.Email, user.ActivationCode.ToString());
                    Status = true;
                return RedirectToAction("UsersList");
                #endregion

            }
            else
            {
                message = "Invalid request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(model);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id,Role,Subscriptions,UserName,Password,Email,FirstName,LastName,ConfirmPassword,BirthDate,PhoneNumber,IsEmailVerified,ActivationCode")] EditViewModel model)
        {
            var user = new Users()
            {  
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ConfirmPassword = model.ConfirmPassword,
                BirthDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber,
                IsEmailVerified = model.IsEmailVerified,
                ActivationCode = model.ActivationCode,
                Subscriptions = model.Subscriptions,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                Role = model.Role
            };


            db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UsersList");
            
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("UsersList");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [NonAction]
        public bool IsEmailExist(string email)
        {
            using (DuomenuBazeEntities db = new DuomenuBazeEntities())
            {
                var v = db.Users.Where(a => a.Email == email).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendeVertificationEmail(string email, string activationCode)
        {
            var verifyUrl = "/RegisterAndLogin/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("zydrunas.testing.codes@gmail.com", "Žydrūnas Grabauskas");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "ManoNaujasSlaptazodis2020";
            string subject = "Jūsų paskyra sėkmingai sukurta!";

            string body = "<br/><br/>Jūsų profilis sėkmingai sukurtas." +
                " Prašome paspausti nuorodą apačioje, kad galėtumėte jį aktyvuoti" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(message);

        }
    }
}
