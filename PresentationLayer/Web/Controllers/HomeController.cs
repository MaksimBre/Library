using Library.BusinessLogicLayer.Managers;
using Library.PresentationLayer.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Library.PresentationLayer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly Users UserManager = new Users();
        private readonly Books BookManager = new Books();


        public ActionResult Index(string search)
        {
            if (search == "")
            {
                IEnumerable<BookModel> models = BookManager.GetAll().Select(x => (BookModel)x);
                return View(models);
            }
            else
            {
                IEnumerable<BookModel> models = BookManager.Search(search).Select(x => (BookModel)x);
                return View(models);
            }
        }
    }
}