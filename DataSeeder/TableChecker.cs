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
                Password = "lol",
                Email = "gagi@gmail.com",
                DateOfBirth = new DateTime(1993, 10, 30),
                DateJoined = DateTime.Now
            };

            library.Users.Insert(user1);
        }
    }
}
