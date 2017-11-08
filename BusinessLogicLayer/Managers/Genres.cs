using Library.BusinessLogicLayer.Managers.Properties;
using Library.BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.BusinessLogicLayer.Managers
{
    public class Genres
    {
        public IEnumerable<Genre> GetAll()
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Genres.GetAll().Select(genre => Map(genre));
            }
        }

        public Genre GetById(int id)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return Map(library.Genres.GetById(id));
            }
        }

        public IEnumerable<Genre> GetByBookId(int id)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Genres.GetByBookId(id).Select(genre => Map(genre));
            }
        }

        public int Add(Genre genre)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                return library.Genres.Insert(Map(genre));
            }
        }

        public void Save(Genre genre)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Genres.Update(Map(genre));
            }
        }

        public void Delete(int id)
        {
            using (DataAccessLayer.DBAccess.Library library = new DataAccessLayer.DBAccess.Library(Settings.Default.LibraryDbConnection))
            {
                library.Genres.Delete(id);
            }
        }

        private Genre Map(DataAccessLayer.Models.Genre dbGenre)
        {
            if (dbGenre == null)
                return null;

            Genre genre = new Genre(dbGenre.Name) { Id = dbGenre.Id };

            return genre;
        }

        private DataAccessLayer.Models.Genre Map(Genre genre)
        {
            if (genre == null)
                throw new ArgumentNullException("genre", "Valid genre is mandatory!");

            return new DataAccessLayer.Models.Genre(genre.Id, genre.Name);
        }
    }
}
