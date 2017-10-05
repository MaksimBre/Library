using System;

namespace Library.DataAccessLayer.Models
{
    public class User
    {
        public User() { }

        public User(int id, string name, string userName, string password, string email, DateTime dateJoined, DateTime? dateOfBirth)
        {
            Id = id;
            Name = name;
            UserName = userName;
            Password = password;
            Email = email;
            DateOfBirth = dateOfBirth;
            DateJoined = dateJoined;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
