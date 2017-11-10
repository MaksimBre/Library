using System;
using System.Collections.Generic;
using System.Linq;
using Library.BusinessLogicLayer.Models;
using Library.BusinessLogicLayer.Managers.Properties;

namespace Library.BusinessLogicLayer.Managers
{
    public class Users
    {
        public IEnumerable<User> GetAll()
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Users.GetAll().Select(user => Map(user));
            }
        }

        public User GetById(int id)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return Map(library.Users.GetById(id));
            }
        }


        public IEnumerable<Role> RoleGetByUser(User user)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Roles.RolesGetByUser(Map(user)).Select(x => Map(x));
            }
        }

        public User GetByLogin(User user)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return Map(library.Users.GetByLogin(Map(user)));
            }
        }

        public User GetByEmail(string email)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return Map(library.Users.GetByEmail(email));
            }
        }

        public int Add(User user)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Users.Insert(Map(user));
            }
        }

        public void Save(User user)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Users.Update(Map(user));
            }
        }

        public void Delete(int id)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Users.Delete(id);
            }
        }

        public void AddUserRole(User user, Role role)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Users.InsertUserRole(Map(user),Map(role));
            }
        }

        public void DeleteAllUserRoles(User user)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Users.DeleteAllUserRoles(Map(user));
            }
        }

        public void AddBookRental(User user, Book book, DateTime returnDate)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Users.InsertBookRentals(Map(user), Map(book), returnDate);
            }
        }

        public IEnumerable<BookRental> GetAllRentalsByUser(User user)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Users.GetAllBookRentalsByUser(Map(user)).Select(x => Map(x));
            }
        }

        public IEnumerable<BookRental> GetAllRentals()
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Users.GetAllBookRentals().Select(x => Map(x));
            }
        }
        
        public void DeleteUserRental(BookRental bookRental)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Users.DeleteBookRentals(Map(bookRental));
            }
        }
        private User Map(DataAccessLayer.Models.User dbUser)
        {
            if (dbUser == null)
                return null;

            User user = new User(dbUser.Name, dbUser.UserName, dbUser.Password, dbUser.Email, dbUser.DateJoined, dbUser.DateOfBirth)
            {
                Id = dbUser.Id
            };

            return user;
        }

        private DataAccessLayer.Models.User Map(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", "Valid user is mandatory!");

            return new DataAccessLayer.Models.User(user.Id, user.Name, user.UserName, user.Password, user.Email, user.DateJoined, user.DateOfBirth);
        }

        private Role Map(DataAccessLayer.Models.Role dbRole)
        {
            if (dbRole == null)
                return null;

            Role role = new Role(dbRole.Name) { Id = dbRole.Id };

            return role;
        }

        private DataAccessLayer.Models.Role Map(Role role)
        {
            if (role == null)
                throw new ArgumentNullException("role", "Valid role is mandatory!");

            return new DataAccessLayer.Models.Role(role.Id, role.Name);
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

        private BookRental Map(DataAccessLayer.Models.BookRental dbBookRental)
        {
            if (dbBookRental == null)
                return null;

            BookRental rental = new BookRental(dbBookRental.UserId, dbBookRental.BookId, dbBookRental.RentalDate, dbBookRental.ReturnDate);
            return rental;
        }

        private DataAccessLayer.Models.BookRental Map(BookRental bookRental)
        {
            if (bookRental == null)
                throw new ArgumentNullException("bookRental", "Valid book rental is mandatory!");

            return new DataAccessLayer.Models.BookRental(bookRental.UserId, bookRental.BookId, bookRental.RentalDate, bookRental.ReturnDate);
        }
    }
}
