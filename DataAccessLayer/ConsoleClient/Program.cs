using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataAccessLayer.Models;

namespace Library.DataAccessLayer.ConsoleClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            //GetAllUsers();
            //GetAllBooks();
            GetAllBookRentals();
        }
        static void GetAllUsers()
        {
            Table.Width = 150;
            Table.Title = "Users";
            Table.ColumnNames = new string[] { "Id", "Name", "User Name", "Role", "Email", "Date joined" };
            Table.Setup();

            using (DBAccess.Library library = new DBAccess.Library(Properties.Settings.Default.LibraryDbConnection))
            {
                foreach (User user in library.Users.GetAll())
                {
                    Table.Insert(1, user.Id.ToString());
                    Table.Insert(2, user.Name);
                    Table.Insert(3, user.UserName);
                    Console.Title = user.Id.ToString();
                    foreach (Role role in library.Roles.RoleGetAllByUser(user))
                    {

                        Table.Insert(4, role.Name);
                    }
                    Table.Insert(5, user.Email);
                    /*string dateOfBirth = Equals(user.DateOfBirth, null) ? "N/A" : user.DateOfBirth.Value.ToShortDateString();
                    Table.Insert(6, dateOfBirth);*/
                    string dateJoined = user.DateJoined.ToShortDateString();
                    Table.Insert(6, dateJoined);
                    Table.NewRow();
                }
            }
        }
        static void GetAllBooks()
        {
            Table.Width = 150;
            Table.Title = "Books";
            Table.ColumnNames = new string[] { "Id", "Title", "No Of Pages", "Stock Count", "Writer", "Genre" };
            Table.Setup();

            using (DBAccess.Library library = new DBAccess.Library(Properties.Settings.Default.LibraryDbConnection))
            {
                foreach (Book book in library.Books.GetAll())
                {
                    Table.Insert(1, book.Id.ToString());
                    Table.Insert(2, book.Title);
                    Table.Insert(3, book.NoOfPages.ToString());
                    Table.Insert(4, book.StockCount.ToString());
                    Writer writer = library.Writers.GetById(book.WriterId);
                    Table.Insert(5, writer.Name);
                    Genre genre = library.Genres.GetByBookId(book);
                    Table.Insert(6, genre.Name);
                }
            }
        }
        static void GetAllBookRentals()
        {
            Table.Width = 150;
            Table.Title = "Book Rentals";
            Table.ColumnNames = new string[] { "Book Title", "Writer", "User", "Rental Date", "Return Date" };
            Table.Setup();

            using(DBAccess.Library library = new DBAccess.Library(Properties.Settings.Default.LibraryDbConnection))
            {
                foreach (User user in library.Users.GetAll()) {
                    foreach (BookRental bookRental in library.Books.GetAllBookRentalsByUser(user)) {
                        Book book = library.Books.GetById(bookRental.BookId);
                        Table.Insert(1,book.Title);

                        Writer writer = library.Writers.GetById(book.WriterId);
                        Table.Insert(2, writer.Name);

                        Table.Insert(3, user.Name);
                        Table.Insert(4, bookRental.RentalDate.ToShortDateString());
                        Table.Insert(5, bookRental.ReturnDate.ToShortDateString());
                    }
                }
            }
        }
    }
}
