using Humanizer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Bson;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AssignmentsProject_2.Models
{
    public class User
    {
        [Required]
        [NotNull]
        public Guid _id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        //[Required]
        [NotNull]
        public string LastName { get; set; } = string.Empty;
        //[Required]
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
        // public string TempPassword { get; set; } //TODO = CONFIRM PASSWORD
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool KeepLoggedIn { get; set; }
        public int Number = 1;
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
            this.Number = u.Number;
        }
        public User() { }
    }
}
