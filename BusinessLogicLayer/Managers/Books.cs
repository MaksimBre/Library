using Library.BusinessLogicLayer.Managers.Properties;
using Library.BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Managers
{
    public class Books
    {
        public IEnumerable<Book> GetAll()
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Books.GetAll().Select(book => Map(book));
            }
        }

        public Book GetById(int id)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return Map(library.Books.GetById(id));
            }
        }

        public IEnumerable<Book> Search(string title)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Books.Search(title).Select(book => Map(book));
            }
        }

        public int Add(Book book)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Books.Insert(Map(book));
            }
        }

        public void Save(Book book)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Books.Update(Map(book));
            }
        }

        public void Delete(Book book)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Books.Delete(Map(book));
            }
        }

        public void AddBookGenre(int bookId, int genreId)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Books.InsertBookGenre(bookId, genreId);
            }
        }

        private Book Map(DataAccessLayer.Models.Book dbBook)
        {
            if (dbBook == null)
                return null;

            Book book = new Book(dbBook.Title, dbBook.NoOfPages, dbBook.StockCount, new Writers().GetById(dbBook.WriterId))
            {
                Id = dbBook.Id
            };
            return book;
        }

        private DataAccessLayer.Models.Book Map(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book", "Valid book is mandatory!");

            return new DataAccessLayer.Models.Book(book.Id, book.Title, book.NoOfPages, book.StockCount, book.Writer.Id);
        }
    }
}
