using Library.BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.PresentationLayer.Web.Models
{
    public class UserModel
    {
        public UserModel() { }

        public UserModel(string name, string userName, string password, string email, DateTime dateJoined, DateTime? dateOfBirth)
        {
            Name = name;
            UserName = userName;
            Password = password;
            Email = email;
            DateOfBirth = dateOfBirth;
            DateJoined = dateJoined;
        }

        public int Id { get; set; }

        [DisplayName("Display name")]
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Name { get; set; }

        [DisplayName("Username")]
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be longer than 6 characters.")]
        public string Password { get; set; }

        [DisplayName("Email address")]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DisplayName("Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateJoined { get; set; }

        [DisplayName("Repeat")]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Remember me")]
        public bool RememberMe { get; set; }

        public string EmailError { get; set; }

        public string NameError { get; set; }

        public string NewPassword { get; set; }

        public string RoleList { get; set; }

        public IEnumerable<RoleModel> RoleList2 { get; set; }

        public static implicit operator User(UserModel um)
        {
            if (um == null)
                return null;

            User user = new User(um.Name, um.UserName, um.Password, um.Email, um.DateJoined, um.DateOfBirth)
            {
                Id = um.Id
            };

            return user;
        }

        public static implicit operator UserModel(User u)
        {
            if (u == null)
                return null;

            UserModel user = new UserModel(u.Name, u.UserName, u.Password, u.Email, u.DateJoined, u.DateOfBirth)
            {
                Id = u.Id
            };

            return user;
        }
    }
}