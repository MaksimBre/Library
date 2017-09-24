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
    public class Books
    {
        private readonly SqlConnection connection;

        internal Books(SqlConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException("connection", "Valid connection is mandatory!");
        }

        public IEnumerable<Book> GetAll()
        {
            using (SqlCommand command = new SqlCommand("EXEC BookGetAll", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Book> books = new List<Book>();
                    while (reader.Read())
                        books.Add(CreateBook(reader));

                    return books;
                }
            }
        }

        public IEnumerable<Book> GetAllBooksByWriter(Writer writer)
        {
            using (SqlCommand command = new SqlCommand("EXEC BookGetAllByWriter @Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = writer.Id;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Book> books = new List<Book>();
                    while (reader.Read())
                        books.Add(CreateBook(reader));

                    return books;
                }
            }
        }

        public IEnumerable<Book> Search(string name)
        {
            using (SqlCommand command = new SqlCommand("EXEC BookSearch @Title ", connection))
            {
                command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = "%" + name + "%";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Book> books = new List<Book>();
                    while (reader.Read())
                        books.Add(CreateBook(reader));

                    return books;
                }
            }
        }

        public Book GetById(int id)
        {
            using (SqlCommand command = new SqlCommand("EXEC BookGetById @Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return CreateBook(reader);

                    return null;
                }
            }
        }

        public int Insert(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book", "Valid book is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC BookInsert @Title, @NoOfPages, @StockCount, @WriterId ", connection))
            {
                command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = book.Title;
                command.Parameters.Add("@NoOfPages", SqlDbType.Int).Value = book.NoOfPages;
                command.Parameters.Add("@StockCount", SqlDbType.Int).Value = book.StockCount;
                command.Parameters.Add("@WriterId", SqlDbType.Int).Value = book.WriterId;

                return (int)command.ExecuteScalar();
            }
        }

        public void Update(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book", "Valid book is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC BookUpdate @Id, @Title, @NoOfPages, @StockCount, @WriterId", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = book.Id;
                command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = book.Title;
                command.Parameters.Add("@NoOfPages", SqlDbType.Int).Value = book.NoOfPages;
                command.Parameters.Add("@StockCount", SqlDbType.Int).Value = book.StockCount;
                command.Parameters.Add("@WriterId", SqlDbType.Int).Value = book.WriterId;

                command.ExecuteNonQuery();
            }
        }

        public void Delete(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book", "Valid book is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC BookDelete @Id ", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = book.Id;
                command.ExecuteNonQuery();
            }
        }

        public void InsertBookGenre(Book book, Genre genre)
        {
            if (book == null)
                throw new ArgumentNullException("book", "Valid book is mandatory!");

            if (genre == null)
                throw new ArgumentNullException("genre", "Valid genre is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC BookInsertBookGenre @BookId, @GenreId ", connection))
            {
                command.Parameters.Add("@BookId", SqlDbType.Int).Value = book.Id;
                command.Parameters.Add("@GenreId", SqlDbType.Int).Value = genre.Id;

                command.ExecuteScalar();
            }
        }

        public void DeleteBookGenre(Book book, Genre genre)
        {
            if (book == null)
                throw new ArgumentNullException("book", "Valid book is mandatory!");

            if (genre == null)
                throw new ArgumentNullException("genre", "Valid genre is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC BookDeleteBookGenre @BookId, @GenreId ", connection))
            {
                command.Parameters.Add("@BookId", SqlDbType.Int).Value = book.Id;
                command.Parameters.Add("@GenreId", SqlDbType.Int).Value = genre.Id;

                command.ExecuteScalar();
            }
        }

        public IEnumerable<BookRental> GetAllBookRentalsByUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", "Valid user is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC BookGetAllRentalsByUser @Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<BookRental> bookRentals = new List<BookRental>();
                    while (reader.Read())
                        bookRentals.Add(CreateBookRental(reader));

                    return bookRentals;
                }
            }
        }

        private Book CreateBook(IDataReader reader)
        {
            return new Book((int)reader["Id"], reader["Title"] as string, (int)reader["NoOfPages"], (int)reader["StockCount"], (int)reader["WriterId"]);
        }

        private BookRental CreateBookRental(IDataReader reader)
        {
            return new BookRental((int)reader["UserId"], (int)reader["BookId"], (DateTime)reader["RentalDate"], (DateTime)reader["ReturnDate"]);
        }

    }
}
