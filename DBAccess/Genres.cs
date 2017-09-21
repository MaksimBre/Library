using Library.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccessLayer.DBAccess
{
    public class Genres
    {
        private readonly SqlConnection connection;

        internal Genres(SqlConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException("connection", "Valid connection is mandatory!");
        }

        public IEnumerable<Genre> GetAll()
        {
            using (SqlCommand command = new SqlCommand("EXEC GenreGetAll", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Genre> genres = new List<Genre>();
                    while (reader.Read())
                        genres.Add(CreateGenre(reader));

                    return genres;
                }
            }
        }

        public Genre GetById(int id)
        {
            using (SqlCommand command = new SqlCommand("EXEC GenreGetById @Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return CreateGenre(reader);

                    return null;
                }
            }
        }

        public Genre GetByBookId(Book book)
        {
            using (SqlCommand command = new SqlCommand("EXEC GenreGetByBookId @Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = book.Id;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return CreateGenre(reader);

                    return null;
                }
            }
        }

        public IEnumerable<Genre> Search(string name)
        {
            using (SqlCommand command = new SqlCommand("EXEC GenreSearch @Name ", connection))
            {
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = "%" + name + "%";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Genre> genres = new List<Genre>();
                    while (reader.Read())
                        genres.Add(CreateGenre(reader));

                    return genres;
                }
            }
        }

        public int Insert(Genre genre)
        {
            if (genre == null)
                throw new ArgumentNullException("genre", "Valid genre is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC GenreInsert @Name ", connection))
            {
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = genre.Name;

                return (int)command.ExecuteScalar();
            }
        }

        public void Update(Genre genre)
        {
            if (genre == null)
                throw new ArgumentNullException("genre", "Valid genre is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC GenreUpdate @Id, @Name", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = genre.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = genre.Name;


                command.ExecuteNonQuery();
            }
        }

        public void Delete(Genre genre)
        {
            if (genre == null)
                throw new ArgumentNullException("genre", "Valid genre is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC GenreDelete @Id ", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = genre.Id;
                command.ExecuteNonQuery();
            }
        }

        private Genre CreateGenre(IDataReader reader)
        {
            return new Genre((int)reader["Id"], reader["Name"] as string);
        }
    }
}
