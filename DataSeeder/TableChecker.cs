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
    }
}
