using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Library.DataAccessLayer.Models;

namespace Library.DataAccessLayer.DBAccess
{
    public class Users
    {
        private readonly SqlConnection connection;

        internal Users(SqlConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException("connection", "Valid connection is mandatory!");
        }

        public IEnumerable<User> GetAll()
        {
            using (SqlCommand command = new SqlCommand("EXEC UserGetAll", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<User> users = new List<User>();
                    while (reader.Read())
                        users.Add(CreateUser(reader));

                    return users;
                }
            }
        }

        public User GetById(int id)
        {
            using (SqlCommand command = new SqlCommand("EXEC UserGetById @Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return CreateUser(reader);

                    return null;
                }
            }
        }

        public User GetByLogin(User user)
        {
            using (SqlCommand command = new SqlCommand("EXEC UserGetByLogin @username, @email, @password", connection))
            {
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = user.UserName;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = user.Email;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = user.Password;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return CreateUser(reader);

                    return null;
                }
            }
        }

        public User GetByEmail(string email)
        {
            using (SqlCommand command = new SqlCommand("EXEC UserGetByEmail @email", connection))
            {
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return CreateUser(reader);

                    return null;
                }
            }
        }

        public IEnumerable<User> Search(string name)
        {
            using (SqlCommand command = new SqlCommand("EXEC UserSearch @Name ", connection))
            {
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = "%" + name + "%";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<User> users = new List<User>();
                    while (reader.Read())
                        users.Add(CreateUser(reader));

                    return users;
                }
            }
        }

        public int Insert(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", "Valid user is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC UserInsert @Name, @UserName ,@Password ,@Email ,@DateOfBirth ,@DateJoined ", connection))
            {
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.Name;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = user.DateOfBirth.Optional();
                command.Parameters.Add("@DateJoined", SqlDbType.Date).Value = user.DateJoined;
                var result = command.ExecuteScalar();
                return (int)result;
            }
        }

        public void Update(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", "Valid user is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC UserUpdate @Id, @Name, @Username, @Password, @Email, @DateOfBirth, @DateJoined", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.Name;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = user.DateOfBirth.Optional();
                command.Parameters.Add("@DateJoined", SqlDbType.Date).Value = user.DateJoined;

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCommand command = new SqlCommand("EXEC UserDelete @Id ", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void InsertUserRole(User user, Role role)
        {
            if (user == null)
                throw new ArgumentNullException("user", "Valid user is mandatory!");

            if (role == null)
                throw new ArgumentNullException("role", "Valid role is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC UserInsertUserRole @UserId, @RoleId ", connection))
            {
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = user.Id;
                command.Parameters.Add("@RoleId", SqlDbType.Int).Value = role.Id;

                command.ExecuteNonQuery();
            }
        }

        public void DeleteUserRole(User user, Role role)
        {
            if (user == null)
                throw new ArgumentNullException("user", "Valid user is mandatory!");

            if (role == null)
                throw new ArgumentNullException("role", "Valid role is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC UserDeleteUserRole @UserId, @RoleId ", connection))
            {
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = user.Id;
                command.Parameters.Add("@RoleId", SqlDbType.Int).Value = role.Id;

                command.ExecuteNonQuery();
            }
        }

        public void DeleteAllUserRoles(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", "Valid user is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC UserDeleteAllUserRoles @UserId", connection))
            {
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = user.Id;

                command.ExecuteNonQuery();
            }
        }

        public void InsertBookRentals(User user, Book book, DateTime returnDate) {
            if (user == null)
                throw new ArgumentNullException("user", "Valid user is mandatory!");

            if (book == null)
                throw new ArgumentNullException("book", "Valid book is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC UserInsertBookRentals @UserId, @BookId, @RentalDate, @ReturnDate ", connection))
            {
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = user.Id;
                command.Parameters.Add("@BookId", SqlDbType.Int).Value = book.Id;
                command.Parameters.Add("@RentalDate", SqlDbType.Date).Value = DateTime.Now;
                command.Parameters.Add("@ReturnDate", SqlDbType.Date).Value = returnDate;

                command.ExecuteNonQuery();
            }
        }

        public void DeleteBookRentals(BookRental bookRental)
        {
            using (SqlCommand command = new SqlCommand("EXEC UserDeleteBookRentals @UserId, @BookId ", connection))
            {
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = bookRental.UserId;
                command.Parameters.Add("@BookId", SqlDbType.Int).Value = bookRental.BookId;

                command.ExecuteNonQuery();
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

        public IEnumerable<BookRental> GetAllBookRentals()
        {
            using (SqlCommand command = new SqlCommand("EXEC BookGetAllRentals", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<BookRental> bookRentals = new List<BookRental>();
                    while (reader.Read())
                        bookRentals.Add(CreateBookRental(reader));

                    return bookRentals;
                }
            }
        }

        private User CreateUser(IDataReader reader)
        {
            return new User((int)reader["Id"], reader["Name"] as string, reader["UserName"] as string, reader["Password"] as string, reader["Email"] as string, (DateTime)reader["DateJoined"], reader["DateOfBirth"].DBNullTo<DateTime?>());
        }

        private BookRental CreateBookRental(IDataReader reader)
        {
            return new BookRental((int)reader["UserId"], (int)reader["BookId"], (DateTime)reader["RentalDate"], (DateTime)reader["ReturnDate"]);
        }
    }
}
