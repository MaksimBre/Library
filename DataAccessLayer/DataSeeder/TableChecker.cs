using System;
using Library.DataAccessLayer.DBAccess;
using Library.DataAccessLayer.Models;

namespace Library.DataAccessLayer.DataSeeder
{
    public static class TableChecker
    {
        public static void CheckUserTable(DBAccess.Library library)
        {
            User user1 = new User()
            {
                Name = "Dragan Ilic",
                UserName = "Gagi",
                Password = "mysecretpassword",
                Email = "gagi@gmail.com",
                DateOfBirth = new DateTime(1993, 10, 30),
                DateJoined = DateTime.Now
            };
            library.Users.Insert(user1);

            User user2 = new User()
            {
                Name = "Donald Trump",
                UserName = "Trump",
                Password = "trumpsecretpassword",
                Email = "trump@gmail.com",
                DateOfBirth = new DateTime(1975, 5, 5),
                DateJoined = DateTime.Now
            };
            user2.Id = library.Users.Insert(user2);

            User user3 = new User()
            {
                Name = "Intruder Int",
                UserName = "Intruder",
                Password = "intruderspassword",
                Email = "intruder@gmail.com",
                DateOfBirth = new DateTime(1993, 10, 30),
                DateJoined = DateTime.Now
            };
            user3.Id = library.Users.Insert(user3);

            user2.Name = "Tottaly not Donald Trump";
            library.Users.Update(user2);

            library.Users.Delete(user3);
        }

        public static void CheckRoleTable(DBAccess.Library library)
        {
            Role role1 = new Role()
            {
                Name = "User"
            };
            library.Roles.Insert(role1);

            Role role2 = new Role()
            {
                Name = "Moderator"
            };
            role2.Id = library.Roles.Insert(role2);

            Role role3 = new Role()
            {
                Name = "God"
            };
            role3.Id = library.Roles.Insert(role3);

            library.Roles.Delete(role2);

            role3.Name = "Administrator";
            library.Roles.Update(role3);
        }

        public static void CheckUserRolesTable(DBAccess.Library library)
        {
            Role role1 = new Role()
            {
                Name = "User"
            };
            role1.Id = library.Roles.Insert(role1);

            Role role2 = new Role()
            {
                Name = "Moderator"
            };
            role2.Id = library.Roles.Insert(role2);

            User user1 = new User()
            {
                Name = "Dragan Ilic",
                UserName = "Gagi",
                Password = "mysecretpassword",
                Email = "gagi@gmail.com",
                DateOfBirth = new DateTime(1993, 10, 30),
                DateJoined = DateTime.Now
            };
            user1.Id = library.Users.Insert(user1);

            User user2 = new User()
            {
                Name = "Paja Patak",
                UserName = "Pataak",
                Password = "kvakva",
                Email = "kva@gmail.com",
                DateOfBirth = new DateTime(2005, 5, 5),
                DateJoined = DateTime.Now
            };
            user2.Id = library.Users.Insert(user2);

            library.Users.InsertUserRole(user1, role1);
            library.Users.InsertUserRole(user2, role2);
        }

        public static void CheckBookTable(DBAccess.Library library)
        {
            Genre genre1 = new Genre()
            {
                Name = "Fantasy Novel"
            };
            genre1.Id = library.Genres.Insert(genre1);

            Writer writer1 = new Writer()
            {
                Name = "J. R. R. Tolken",
                Biography = "John Ronald Reuel Tolkien (3 January 1892 – 2 September 1973) was an English writer, poet, philologist, and university professor who is best known as the author of the classic high-fantasy works The Hobbit, The Lord of the Rings, and The Silmarillion.",
                NoOfBooks = 32
            };
            writer1.Id = library.Writers.Insert(writer1);

            Book book1 = new Book()
            {
                Title = "Fellowship of the Ring",
                NoOfPages = 535,
                StockCount = 20,
                WriterId = writer1.Id
            };
            book1.Id = library.Books.Insert(book1);
            library.Books.InsertBookGenre(book1,genre1);
        }

        public static void CheckRentalTable(DBAccess.Library library)
        {
            User user1 = new User()
            {
                Name = "Tosa",
                UserName = "Tole",
                Password = "nacudatikazem",
                Email = "tosa@gmail.com",
                DateOfBirth = new DateTime(1995,6,6),
                DateJoined = DateTime.Now
            };
            user1.Id = library.Users.Insert(user1);

            User user2 = new User()
            {
                Name = "Milan",
                UserName = "Milan",
                Password = "nacudatikazem",
                Email = "miki@gmail.com",
                DateOfBirth = new DateTime(1987, 8, 15),
                DateJoined = DateTime.Now
            };
            user2.Id = library.Users.Insert(user2);

            Writer writer1 = new Writer()
            {
                Name = "J.R.R. Tolken",
                Biography = "Lorem Ipsum"
            };
            writer1.Id = library.Writers.Insert(writer1);

            Book book1 = new Book()
            {
                Title = "Simarilion",
                NoOfPages = 380,
                StockCount = 30,
                WriterId = writer1.Id
            };
            book1.Id = library.Books.Insert(book1);

            library.Users.InsertBookRentals(user1, book1, new DateTime(2017,9,30));
        }
    }
}
