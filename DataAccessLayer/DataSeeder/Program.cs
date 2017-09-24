using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataAccessLayer.DBAccess;

namespace Library.DataAccessLayer.DataSeeder
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (DBAccess.Library library = new DBAccess.Library(Properties.Settings.Default.LibraryDbConnection))
            {
                //TableChecker.CheckUserTable(library);
                //TableChecker.CheckRoleTable(library);
                //TableChecker.CheckUserRolesTable(library);
                //TableChecker.CheckBookTable(library);
                TableChecker.CheckRentalTable(library);
            }

        }
    }
}
