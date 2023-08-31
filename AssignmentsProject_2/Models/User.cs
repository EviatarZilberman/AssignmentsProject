using Humanizer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Bson;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Utilities;

namespace AssignmentsProject_2.Models
{
    public class User
    {
        [Required]
        [NotNull]
        public Guid _id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        [NotNull]
        public string LastName { get; set; } = string.Empty;
        [NotNull]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [NotNull]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [NotNull]
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool KeepLoggedIn { get; set; }
        public User (User u)
        {
            this._id = u._id;
            this.FirstName = u.FirstName;
            this.LastName = u.LastName;
            this.UserName = u.UserName;
            this.Email = u.Email;
            this.Password = u.Password;
            this.Assignments = u.Assignments;
            this.CreationDate = u.CreationDate;
            this.KeepLoggedIn = u.KeepLoggedIn;
        }
        public User() { }

        public static CoreReturns ValidateUser (User user)
        {
            if (user == null) return CoreReturns.IS_NULL;
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrWhiteSpace(user.Email)) return CoreReturns.EMAIL_IS_NULL_OR_EMPTY_OR_WHITESPACE;
            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrWhiteSpace(user.Password)) return CoreReturns.PASSWORD_IS_NULL_OR_EMPTY_OR_WHITESPACE;
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrWhiteSpace(user.UserName)) return CoreReturns.USERNAME_IS_NULL_OR_EMPTY_OR_WHITESPACE;
            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrWhiteSpace(user.UserName)) return CoreReturns.FIRSTNAME_IS_NULL_OR_EMPTY_OR_WHITESPACE;
            if (string.IsNullOrEmpty(user.LastName) || string.IsNullOrWhiteSpace(user.UserName)) return CoreReturns.LASTNAME_IS_NULL_OR_EMPTY_OR_WHITESPACE;
            
            return CoreReturns.SUCCESS;
        }
    }
}
