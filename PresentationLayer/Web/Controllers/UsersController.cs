using Library.BusinessLogicLayer.Managers;
using Library.PresentationLayer.Web.Helpers;
using Library.PresentationLayer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Library.PresentationLayer.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly Users UserManager = new Users();
        private readonly Roles RoleManager = new Roles();

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            IEnumerable<UserModel> models = UserManager.GetAll().Select(x => (UserModel)x);
            return View(models);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(UserModel model)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("Email");
            ModelState.Remove("DateJoined");

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Users");
            }

            UserModel newModel = UserManager.GetById(model.Id);
            newModel.Name = model.Name;
            newModel.UserName = model.UserName;
            newModel.DateOfBirth = model.DateOfBirth;

            UserManager.Save(newModel);
            return RedirectToAction("Index", "Users");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditRole(int id)
        {
            UserModel model = UserManager.GetById(id);

            IEnumerable<RoleModel> userRoles = UserManager.RoleGetByUser(model).Select(x => (RoleModel)x);
            IEnumerable<RoleModel> allRoles = RoleManager.GetAll().Select(x => (RoleModel)x);
            List<RoleModel> tempRoles = new List<RoleModel>();

            foreach(var role in allRoles)
            {
                foreach (var uRole in userRoles) {
                    if (role.Name == uRole.Name) {
                        role.IsChecked = true;
                    }
                }
                tempRoles.Add(role);
            }
            model.RoleList2 = tempRoles;
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult EditRole(UserModel model)
        {
            UserModel newModel = UserManager.GetById(model.Id);
            UserManager.DeleteAllUserRoles(newModel);
            string roleList = model.RoleList;

            if(roleList == "" || roleList == null)
            {
                return RedirectToAction("Index", "Users");
            }
            //roleList.Remove(0,roleList.Length-1);
            string[] roles = roleList.Split('/');
            //int[] roleIds = Array.ConvertAll(roles, int.Parse);
            //int[] roleIds = roles.Select(int.Parse).ToArray();
            //int[] roleIds = Array.ConvertAll(roles, s => int.Parse(s));

            for (int i = 0; i < roles.Length-1; i++)
            {
                Int32.TryParse(roles[i], out int x);
                RoleModel roleModel = RoleManager.GetById(x);
                UserManager.AddUserRole(newModel, roleModel);
            }
            return RedirectToAction("Index", "Users");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            UserManager.Delete(id);
            return RedirectToAction("Index", "Users");
        }

        [Authorize]
        public ActionResult MyRentals()
        {
            UserModel userModel = UserManager.GetByEmail(UserIdentity.Email());
            IEnumerable<BookRentalModel> rentals = UserManager.GetAllRentalsByUser(userModel).Select(x => (BookRentalModel)x);

            return View(rentals);
        }
    }
}