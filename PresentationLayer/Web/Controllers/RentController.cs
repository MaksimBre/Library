using Library.BusinessLogicLayer.Managers;
using Library.PresentationLayer.Web.Helpers;
using Library.PresentationLayer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.PresentationLayer.Web.Controllers
{
    public class RentController : Controller
    {
        private readonly Users UserManager = new Users();
        private readonly Books BookManager = new Books();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RentBook(int id)
        {
            UserModel userModel = UserManager.GetByEmail(UserIdentity.Email());
            BookModel bookModel = BookManager.GetById(id);
            DateTime returnDate = DateTime.Now.AddMonths(1);

            UserManager.AddBookRental(userModel, bookModel, returnDate);
            TempData["BookRental"] = userModel.Name + "&" + bookModel.Title + "&" + returnDate.ToShortDateString();

            return RedirectToAction("Index", "Home");
        }
    }
}