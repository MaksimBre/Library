using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Library.BusinessLogicLayer.Managers;
using Library.PresentationLayer.Web.Models;
using Library.PresentationLayer.Web.Helpers;
using PagedList;

namespace Library.PresentationLayer.Web.Controllers
{
    public class WriterController : Controller
    {
        private readonly Writers WriterManager = new Writers();

        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            IEnumerable<WriterModel> models = WriterManager.GetAll().Select(x => (WriterModel)x);
            PagedList<WriterModel> modelsPage = new PagedList<WriterModel>(models, page, pageSize);

            if (Request.IsAuthenticated)
            {
                if (UserIdentity.IsInRole("Librarian,Administrator"))
                {
                    return View("Manage", modelsPage);
                }
            }
            return View(modelsPage);
        }

        [Authorize(Roles = "Librarian,Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Librarian,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WriterModel writerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(writerModel);
            }

            WriterManager.Add(writerModel);

            return RedirectToAction("Index", "Writer");
        }

        [Authorize(Roles = "Librarian,Administrator")]
        [HttpPost]
        public ActionResult Save(WriterModel writerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(writerModel);
            }

            WriterManager.Save(writerModel);
            return RedirectToAction("Index", "Writer");
        }

        [Authorize(Roles = "Librarian,Administrator")]
        public ActionResult Delete(int id)
        {
            WriterManager.Delete(id);
            return RedirectToAction("Index", "Writer");
        }
    }
}