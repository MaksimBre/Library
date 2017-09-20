using System;
using System.Data.SqlClient;

namespace Library.DataAccessLayer.DBAccess
{
    public class Library : IDisposable
    {
        private readonly SqlConnection connection;

        public Users Users { get; set; }
        public Roles Roles { get; set; }
        /*public Contacts Contacts { get; set; }
        public Phones Phones { get; set; }
        public PhoneTypes PhoneTypes { get; set; }
        public Countries Countries { get; set; }
        public Addresses Addresses { get; set; }
        public AddressTypes AddressTypes { get; set; }*/

        public Library(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Valida connection string is mandatory!", "connectionString");

            connection = new SqlConnection(connectionString);
            connection.Open();

            Users = new Users(connection);
            Roles = new Roles(connection);
            /*Contacts = new Contacts(connection);
            Phones = new Phones(connection);
            PhoneTypes = new PhoneTypes(connection);
            Countries = new Countries(connection);
            Addresses = new Addresses(connection);
            AddressTypes = new AddressTypes(connection);*/
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}