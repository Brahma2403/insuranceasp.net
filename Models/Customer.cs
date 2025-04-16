using System.ComponentModel.DataAnnotations;

namespace insuranceApp1.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        //[ForeignKey("Users")]
        //public int UserId { get; set; }
        //public virtual Users Users { get; set; }

        [Required(ErrorMessage = "Name is required"), MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required"), MaxLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required."), MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required."), MaxLength(10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Phone { get; set; }
         
        //public virtual ICollection<Policy> Policies { get; set; }
        public virtual ICollection<PremiumCalculation> Calculations { get; set; }
    }
}