using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "FullName is required.")]
        public string FullName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }
      
    }
}
