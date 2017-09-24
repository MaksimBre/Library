using System;

namespace Library.DataAccessLayer.Models
{
    public class User
    {
        public User() { }

        public User(int id, string name, string username, string password, string email, DateTime? dob, DateTime dj)
        {
            Id = id;
            Name = name;
            UserName = username;
            Password = password;
            Email = email;
            DateOfBirth = dob;
            DateJoined = dj;
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
