using System.Collections.Generic;
using System.Linq;
using Library.BusinessLogicLayer.Managers;
using Library.PresentationLayer.Web.Models;
using System.Web.Mvc;
using System;
using PagedList;

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
            ModelState.Remove("Writer.Name");
            ModelState.Remove("Writer.Biography");
            if (!ModelState.IsValid)
            { 
                return RedirectToAction("Index", "Home");
            }
            bookModel.Writer = WriterManager.GetById(bookModel.Writer.Id);
            BookManager.Save(bookModel);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Librarian,Administrator")]
        public ActionResult EditBookGenre(int id)
        {
            BookModel bookModel = BookManager.GetById(id);

            IEnumerable<GenreModel> bookGenres = GenreManager.GetByBookId(bookModel.Id).Select(x => (GenreModel)x);
            IEnumerable<GenreModel> allGenres = GenreManager.GetAll().Select(x => (GenreModel)x);
            List<GenreModel> tempGenres = new List<GenreModel>();

            foreach (var genre in allGenres)
            {
                foreach (var bGenre in bookGenres)
                {
                    if (genre.Name == bGenre.Name)
                    {
                        genre.IsChecked = true;
                    }
                }
                tempGenres.Add(genre);
            }
            bookModel.ListOfGenres = tempGenres;

            return View(bookModel);
        }

        [Authorize(Roles = "Librarian,Administrator")]
        [HttpPost]
        public ActionResult EditBookGenre(BookModel model)
        {
            BookModel bookModel = BookManager.GetById(model.Id);

            BookManager.DeleteAllBookGenres(bookModel);

            string genreList = model.GenresList;
            if (genreList == "" || genreList == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string[] genres = genreList.Split('/');

            for (int i = 0; i < genres.Length - 1; i++)
            {
                Int32.TryParse(genres[i], out int x);
                GenreModel genreModel = GenreManager.GetById(x);
                BookManager.AddBookGenre(bookModel.Id, genreModel.Id);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Librarian,Administrator")]
        public ActionResult Delete(int id)
        {
            BookManager.Delete(id);
            return RedirectToAction("Index", "Home");
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
        public ActionResult GenreEdit(int page = 1, int pageSize = 10)
        {
            IEnumerable<GenreModel> models = GenreManager.GetAll().Select(x => (GenreModel)x);
            PagedList<GenreModel> modelsPage = new PagedList<GenreModel>(models, page, pageSize);
            return View(modelsPage);
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