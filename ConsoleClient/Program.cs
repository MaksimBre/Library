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
            Table.Width = 200;
            Table.Title = "Users";
            Table.ColumnNames = new string[] { "Id", "Name", "User Name", "Password", "Email", "Date of birth", "Date joined" };
            Table.Setup();

            using (DBAccess.Library library = new DBAccess.Library(Properties.Settings.Default.LibraryDbConnection))
            {
                foreach (User user in library.Users.GetAll())
                {
                    Table.Insert(1, user.Id.ToString());
                    Table.Insert(2, user.Name);
                    Table.Insert(3, user.UserName);
                    Table.Insert(4, user.Password);
                    Table.Insert(5, user.Email);
                    Table.Insert(6, user.DateOfBirth.ToString());
                    Table.Insert(7, user.DateJoined.ToString());
                }
            }
        }
    }
}
