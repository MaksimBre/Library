using System;
using System.Collections.Generic;
using Library.BusinessLogicLayer.Managers.Properties;
using Library.BusinessLogicLayer.Models;
using System.Linq;

namespace Library.BusinessLogicLayer.Managers
{
    public class Writers
    {
        public IEnumerable<Writer> GetAll()
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Writers.GetAll().Select(writer => Map(writer));
            }
        }

        public Writer GetById(int id)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return Map(library.Writers.GetById(id));
            }
        }

        public int Add(Writer writer)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Writers.Insert(Map(writer));
            }
        }

        public void Save(Writer writer)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Writers.Update(Map(writer));
            }
        }

        public void Delete(int id)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Writers.Delete(id);
            }
        }

        private Writer Map(DataAccessLayer.Models.Writer dbWriter)
        {
            if (dbWriter == null)
                return null;

            Writer writer = new Writer(dbWriter.Name, dbWriter.Biography, dbWriter.NoOfBooks) { Id = dbWriter.Id };
            return writer;
        }

        private DataAccessLayer.Models.Writer Map(Writer writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer", "Valid writer is mandatory!");

            return new DataAccessLayer.Models.Writer(writer.Id, writer.Name, writer.Biography, writer.NoOfBooks);
        }
    }
}
