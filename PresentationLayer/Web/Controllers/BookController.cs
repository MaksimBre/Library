using System.Collections.Generic;
using System.Linq;
using Library.BusinessLogicLayer.Managers;
using Library.PresentationLayer.Web.Models;
using System.Web.Mvc;
using System;

namespace Library.PresentationLayer.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly Books BookManager = new Books();
        private readonly Writers WriterManager = new Writers();
        private readonly Genres GenreManager = new Genres();

        public ActionResult Index()
        {
            IEnumerable<BookModel> models = BookManager.GetAll().Select(x => (BookModel)x);
            return View(models);
        }

        [Authorize(Roles = "Librarian,Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Librarian,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookModel bookModel)
        {
            ModelState.Remove("Writer.Name");
            ModelState.Remove("Writer.Biography");
            if (!ModelState.IsValid)
            {
                return View(bookModel);
            }
            bookModel.Writer = WriterManager.GetById(bookModel.Writer.Id);
            int id = BookManager.Add(bookModel);
            string[] genres = bookModel.GenresList.Split('/');
            for(var i = 0; i < genres.Length-1; i++)
            {
                BookManager.AddBookGenre(id, Convert.ToInt32(genres[i]));
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Librarian,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BookModel bookModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookModel);
            }

            BookManager.Save(bookModel);
            return View();
        }

        [Authorize(Roles = "Librarian,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            WriterManager.Delete(id);
            return View();
        }

        [Authorize(Roles = "Librarian,Administrator")]
        [HttpPost]
        public ActionResult GenreAdd(GenreModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("GenreEdit", "Book");
            }
            GenreManager.Add(model);
            return RedirectToAction("GenreEdit", "Book");
            
        }

        [Authorize(Roles = "Librarian,Administrator")]
        public ActionResult GenreEdit()
        {
            IEnumerable<GenreModel> models = GenreManager.GetAll().Select(x => (GenreModel)x);
            return View(models);
        }

        [Authorize(Roles = "Librarian,Administrator")]
        [HttpPost]
        public ActionResult GenreEdit(GenreModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            GenreManager.Save(model);
            return RedirectToAction("GenreEdit", "Book");
        }

        [Authorize(Roles = "Librarian,Administrator")]
        public ActionResult GenreDelete(int id)
        {
            GenreManager.Delete(id);
            return RedirectToAction("GenreEdit", "Book");
        }
    }
}