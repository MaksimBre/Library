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
    }
}
