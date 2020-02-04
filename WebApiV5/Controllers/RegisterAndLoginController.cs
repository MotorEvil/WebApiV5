using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApiV5.Models;
using System.Web.Security;
using WebApiV5.Models.ViewModels;
using RegistrationAndLogin.Models;

namespace WebApiV5.Controllers
{
    public class RegisterAndLoginController : Controller
    {
        [HttpGet]
        public ActionResult Registration()

        {
            return View();
        }

        //Registration POST action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified, ActivationCode")] UserRegistration model)

        {
            var user = new Users() {
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                ConfirmPassword = model.ConfirmPassword,
                ActivationCode = model.ActivationCode,
                IsEmailVerified = model.IsEmailVerified,
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
                using (DuomenuBazeEntities db = new DuomenuBazeEntities())
                {

                    db.Users.Add(user);
                    db.SaveChanges();

                    //Send email to user
                    SendeVerificationEmail(user.Email, user.ActivationCode.ToString());
                    message = "Jūs užsiregistravote sėkmingai. Aktyvacijos nuoroda buvo išsiųstas jūsų paštu:" +
                        user.Email;
                    Status = true;
                }
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

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (DuomenuBazeEntities dc = new DuomenuBazeEntities())
            {
                dc.Configuration.ValidateOnSaveEnabled = false; 

                var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }

        //Login 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (DuomenuBazeEntities dc = new DuomenuBazeEntities())
            {
                var v = dc.Users.Where(a => a.Email == login.Email).FirstOrDefault();
                if (v != null)
                {
                    if (!v.IsEmailVerified)
                    {
                        ViewBag.Message = "Prašome patvirtinti slaptažodį";
                        return View();
                    }

                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.Email, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        message = "Neteisingi duomenys";
                    }
                }
                else
                {
                    message = "Neteisingi duomenys";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
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
        public void SendeVerificationEmail(string email, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/RegisterAndLogin/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("zydrunas.testing.codes@gmail.com", "Žydrūnas Grabauskas");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "ManoNaujasSlaptazodis2020";

            string subject = "";
            string body = "";

            if (emailFor == "VerifyAccount")
            {
                subject = "Jūsų paskyra sėkmingai sukurta!";
                body = "<br/><br/>Jūsų profilis sėkmingai sukurtas." +
                    " Prašome paspausti nuorodą apačioje, kad galėtumėte jį aktyvuoti" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }
            

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


        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            
            string message = "";
            //bool status = false;

            using (DuomenuBazeEntities db = new DuomenuBazeEntities())
            {
                var account = db.Users.Where(a => a.Email == email).FirstOrDefault();
                if (account != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendeVerificationEmail(account.Email, resetCode, "ResetPassword");
                    account.ResetPasswordCode = resetCode;

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "Reset password link has been sent to your email id.";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (DuomenuBazeEntities dc = new DuomenuBazeEntities())
            {
                var user = dc.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (DuomenuBazeEntities db = new DuomenuBazeEntities())
                {
                    var user = db.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = Crypto.Hash(model.NewPassword);
                        user.ResetPasswordCode = "";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }





    }
}