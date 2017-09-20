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
            using (SqlCommand command = new SqlCommand("SELECT * FROM Users ", connection))
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
            using (SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Id = @Id", connection))
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
        /*public IEnumerable<Contact> Search(string name)
        {
            using (SqlCommand command = new SqlCommand("SELECT *  FROM Contacts WHERE Name LIKE '@Name' ", connection))//?
            {
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name + "%";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Contact> contacts = new List<Contact>();
                    while (reader.Read())
                        contacts.Add(CreateContact(reader));

                    return contacts;
                }
            }
        }*/

        public int Insert(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", "Valid user is mandatory!");

            using (SqlCommand command = new SqlCommand("INSERT INTO Users (Name, UserName, Password, Email, DateOfBirth, DateJoined) " +
                                                       "VALUES (@Name, @UserName, @Password, @Email ,@DateOfBirth ,@DateJoined)" +
                                                       "SELECT CAST(SCOPE_IDENTITY() AS int)", connection))
            {
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.Name;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = user.DateOfBirth.Optional();
                command.Parameters.Add("@DateJoined", SqlDbType.Date).Value = user.DateJoined;

                return (int)command.ExecuteScalar();
            }
        }

        /*public void Update(Contact contact)
        {
            if (contact == null)
                throw new ArgumentNullException("contact", "Valid contact is mandatory!");

            using (SqlCommand command = new SqlCommand("UPDATE Contacts " +
                                                       "SET Name = @Name, Picture=@Picture, DateOfBirth=@DateOfBirth " +
                                                       "WHERE Id = @Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = contact.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = contact.Name;
                command.Parameters.Add("@Picture", SqlDbType.Image).Value = contact.Picture.Optional();
                command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = contact.DateOfBirth.Optional();

                command.ExecuteNonQuery();
            }
        }*/

        public void Delete(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", "Valid user is mandatory!");

            using (SqlCommand command = new SqlCommand("DELETE FROM Users " +
                                                       "WHERE Id = @Id ", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;
                command.ExecuteNonQuery();
            }
        }

        private User CreateUser(IDataReader reader)
        {
            return new User((int)reader["Id"], reader["Name"] as string, reader["UserName"] as string, reader["Password"] as string, reader["Email"] as string, reader["DateOfBirth"].DBNullTo<DateTime?>(), (DateTime)reader["DateJoined"]);


        }

    }
}
