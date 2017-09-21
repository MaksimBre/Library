using System;
using System.Data.SqlClient;

namespace Library.DataAccessLayer.DBAccess
{
    public class Library : IDisposable
    {
        private readonly SqlConnection connection;

        public Users Users { get; set; }
        public Roles Roles { get; set; }
        public Books Books { get; set; }
        public Writers Writers { get; set; }
        public Genres Genres { get; set; }

        public Library(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Valida connection string is mandatory!", "connectionString");

            connection = new SqlConnection(connectionString);
            connection.Open();

            Users = new Users(connection);
            Roles = new Roles(connection);
            Books = new Books(connection);
            Writers = new Writers(connection);
            Genres = new Genres(connection);
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}