using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insuranceApp1.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "UserName is required"), MaxLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required."), MinLength(8)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?\""{}|<>]).+$", ErrorMessage = "Password must contain at least one capital letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

    }
}