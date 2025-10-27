// Models/User.cs
using System.ComponentModel.DataAnnotations;


namespace FullStackFinalProject.Api.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required, StringLength(100)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User";
    }
}
