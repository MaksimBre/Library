using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Security;
using Library.PresentationLayer.Web.Models;
using Library.BusinessLogicLayer.Managers;
using Library.BusinessLogicLayer.Models;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Library.PresentationLayer.Web.Helpers;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly Users UserManager = new Users();
        private readonly Library.BusinessLogicLayer.Managers.Roles RoleManager = new Library.BusinessLogicLayer.Managers.Roles();

        public AccountController() { }

        [Authorize]
        public ActionResult Manage()
        {
            string email = UserIdentity.Email();
            UserModel model = UserManager.GetByEmail(email);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Manage(UserModel model)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("Email");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserModel newModel = UserManager.GetById(model.Id);
            newModel.Name = model.Name;
            newModel.UserName = model.UserName;
            newModel.DateOfBirth = model.DateOfBirth;

            UserManager.Save(newModel);
            return View();
        }

        [Authorize]
        public ActionResult ChangeEmail()
        {
            string email = UserIdentity.Email();
            UserModel model = UserManager.GetByEmail(email);
            model.Password = "";
            model.Email = "";
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangeEmail(UserModel model)
        {
            ModelState.Remove("Name");
            ModelState.Remove("UserName");
            if (!ModelState.IsValid)
                return View(model);

            UserModel newModel = UserManager.GetById(model.Id);

            var password = model.Password;
            using (MD5 md5Hash = MD5.Create())
            {
                model.Password = GetMd5Hash(md5Hash, password);
            }

            if (model.Password == newModel.Password)
            {
                if(model.Email == newModel.Email)
                {
                    ModelState.AddModelError("Email","New email address can't be you old email address.");
                    return View(model);
                }

                newModel.Email = model.Email;
                UserManager.Save(newModel);

                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                FormsAuthentication.SignOut();
                TempData["LogIn"] = "EmailChanged";
                return RedirectToAction("LogIn", "Account");
            }

            return View();
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            string email = UserIdentity.Email();
            UserModel model = UserManager.GetByEmail(email);
            model.Password = "";
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(UserModel model)
        {
            ModelState.Remove("UserName");
            ModelState.Remove("Name");
            ModelState.Remove("Email");
            ModelState.Remove("ConfirmPassword");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserModel newModel = UserManager.GetById(model.Id);

            var password = HashIt(model.Password);

            if (password != newModel.Password)
            {
                ModelState.AddModelError("Password", "Old password does not match.");
                return View(model);
            }
            if (model.NewPassword == "" || model.NewPassword == null)
            {
                ModelState.AddModelError("newPassword", "New password must not be empty.");
                return View(model);
            }
            if (model.NewPassword.Length < 6 || model.NewPassword.Length > 50)
            {
                ModelState.AddModelError("newPassword", "New password must be longer than 6 characters.");
                return View(model);
            }
            if (model.ConfirmPassword == "")
            {
                ModelState.AddModelError("ConfirmPassword", "Password confirmation must not be empty.");
                return View(model);
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return View(model);
            }

            if(model.Password == model.NewPassword)
            {
                ModelState.AddModelError("NewPassword","New password can't be your old password.");
                return View(model);
            }


            newModel.Password = HashIt(model.NewPassword);
            UserManager.Save(newModel);

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            TempData["LogIn"] = "PassChanged";
            return RedirectToAction("LogIn", "Account");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserModel model, string returnUrl)
        {
            ModelState.Remove("Name");
            ModelState.Remove("UserName");
            ModelState.Remove("ConfirmPassword");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var password = model.Password;
            using (MD5 md5Hash = MD5.Create())
            {
                model.Password = GetMd5Hash(md5Hash, password);
            }
            UserModel userModel = new UserModel()
            {
                Name = "TEMPORARY_NAME",
                UserName = "TEMPORARY_USERNAME",
                Email = model.Email,
                Password = model.Password,
                DateJoined = DateTime.Now
            };

            UserModel dbUserModel = UserManager.GetByLogin(userModel);

            if (dbUserModel != null)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                IEnumerable<Role> roles = UserManager.RoleGetByUser(dbUserModel);
                string roleString = "";
                foreach (Role r in roles)
                {
                    roleString += r.Name + ",";
                }

                DateTime timeout2 = DateTime.Now.AddMonths(3);
                DateTime timeout = model.RememberMe ? timeout2 : DateTime.MinValue;

                var ticket = new FormsAuthenticationTicket(1, dbUserModel.Email + "/" + dbUserModel.Name, DateTime.Now, timeout2, model.RememberMe, roleString);

                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
                {
                    Expires = timeout
                };
                HttpContext.Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }
        
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return View(model);
            }
            if (ModelState.IsValid)
            {

                model.DateJoined = DateTime.Now;
                var password = model.Password;
                using (MD5 md5Hash = MD5.Create())
                {
                    model.Password = GetMd5Hash(md5Hash, password);
                }
                int id = UserManager.Add(model);

                if (id == -1)
                {
                    ModelState.AddModelError("NameError", "User with this name already exists.");
                    return View(model);
                }
                if (id == -2)
                {
                    ModelState.AddModelError("EmailError", "Account registered with this email already exists.");
                    return View(model);
                }

                model.Id = id;
                Role userRole = RoleManager.GetByName("User");
                UserManager.AddUserRole(model, userRole);

                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public string HashIt(string pass)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, pass);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Helpers
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion
    }
}