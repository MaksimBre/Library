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
            using(DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Users.GetAll().Select(user => Map(user));
            }
        }

        private User Map(DataAccessLayer.Models.User dbUser)
        {
            if (dbUser == null)
                return null;

            User user = new User(dbUser.Name, dbUser.UserName, dbUser.Password, dbUser.Email, dbUser.DateJoined, dbUser.DateOfBirth);
            user.Id = dbUser.Id;

            return user;
        }

        private DataAccessLayer.Models.User Map(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user","Valid user is mandatory!");

            return new DataAccessLayer.Models.User(user.Id,user.Name, user.UserName, user.Password, user.Email, user.DateJoined, user.DateOfBirth);
        }
    }
}
