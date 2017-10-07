using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.BusinessLogicLayer.Models;
using Library.BusinessLogicLayer.Managers.Properties;

namespace Library.BusinessLogicLayer.Managers
{
    public class Roles
    {
        public IEnumerable<Role> GetAll()
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Roles.GetAll().Select(role => Map(role));
            }
        }

        public Role GetById(int id)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return Map(library.Roles.GetById(id));
            }
        }

        public int Add(Role role)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Roles.Insert(Map(role));
            }
        }

        public void Save(Role role)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Roles.Update(Map(role));
            }
        }

        public void Delete(Role role)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Roles.Delete(Map(role));
            }
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
    }
}
