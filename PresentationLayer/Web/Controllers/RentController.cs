using Library.BusinessLogicLayer.Managers;
using Library.PresentationLayer.Web.Helpers;
using Library.PresentationLayer.Web.Models;
using PagedList;
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

        [Authorize(Roles = "Librarian,Administrator")]
        public ActionResult Index(int page = 1, int pageSize = 15)
        {
            IEnumerable<BookRentalModel> rentals = UserManager.GetAllRentals().Select(x => (BookRentalModel)x);
            PagedList<BookRentalModel> modelsPage = new PagedList<BookRentalModel>(rentals, page, pageSize);
            return View(modelsPage);
        }

        public ActionResult RentBook(int id)
        {
            UserModel userModel = UserManager.GetByEmail(UserIdentity.Email());
            BookModel bookModel = BookManager.GetById(id);
            DateTime returnDate = DateTime.Now.AddMonths(1);

            IEnumerable<BookRentalModel> allRentals = UserManager.GetAllRentalsByUser(userModel).Select(x => (BookRentalModel)x);

            foreach (BookRentalModel rental in allRentals)
            {
                if (rental.BookId == bookModel.Id)
                {
                    TempData["BookRentalFail"] = userModel.Name + "&" + bookModel.Title;
                    return RedirectToAction("Index", "Home");
                }
            }

            UserManager.AddBookRental(userModel, bookModel, returnDate);
            TempData["BookRental"] = userModel.Name + "&" + bookModel.Title + "&" + returnDate.ToShortDateString();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UserRentals(int id, int page = 1, int pageSize = 10)
        {
            UserModel userModel = UserManager.GetById(id);
            IEnumerable<BookRentalModel> rentals = UserManager.GetAllRentalsByUser(userModel).Select(x => (BookRentalModel)x);
            PagedList<BookRentalModel> modelsPage = new PagedList<BookRentalModel>(rentals, page, pageSize);
            return View(modelsPage);
        }

        [Authorize(Roles = "Librarian,Administrator")]
        [HttpPost]
        public ActionResult Return(BookRentalModel model)
        {
            UserManager.DeleteUserRental(model);
            return RedirectToAction("Index", "Rent");
        }
    }
}