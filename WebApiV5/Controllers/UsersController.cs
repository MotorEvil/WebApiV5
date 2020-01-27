using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiV5.Models;
using System.Net;
using System.Data.Entity;
using System.Data;
using System.Net.Mail;

namespace WebApiV5.Controllers
{
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
        public ActionResult Create([Bind(Include = "UserName,FirstName,LastName,Password,ConfirmPassword,BirthDate,PhoneNumber,Email,Subscriptions", Exclude = "IsEmailVerified, ActivationCode")]Users users)
        {
            bool Status = false;
            string message = "";

            //Model Validation
            if (ModelState.IsValid)
            {
                #region Email is already exist
                var isExist = IsEmailExist(users.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email alrerady exist");
                    return View(users);
                }
                #endregion

                #region Generate Activation Code
                users.ActivationCode = Guid.NewGuid();
                #endregion

                #region Password hashing
                users.Password = Crypto.Hash(users.Password);
                users.ConfirmPassword = Crypto.Hash(users.ConfirmPassword);
                #endregion

                users.IsEmailVerified = false;

                #region Save to DataBAse
                    db.Users.Add(users);
                    db.SaveChanges();

                    //Send email to user
                    SendeVertificationEmail(users.Email, users.ActivationCode.ToString());
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
            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("UsersList");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("UsersList");
            }
            catch
            {
                return View();
            }
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
