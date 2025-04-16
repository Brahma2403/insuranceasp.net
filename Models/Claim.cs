using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insuranceApp1.Models
{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        [ForeignKey("policy")]
        public int PolicyId { get; set; }
        public virtual Policy Policy { get; set; }  //reference navigation

        [Required(ErrorMessage = "ClaimAmount is required")]
        public double ClaimAmount { get; set; }

        [Required(ErrorMessage = "ClaimStatus is required"), MaxLength(100)]
        public string ClaimStatus { get; set; }

        [Required(ErrorMessage = "SubmissionDate is required")]
        public DateOnly SubmissionDate { get; set; }

        [Required(ErrorMessage = "SettlementDate is required")]
        public DateOnly SettlementDate { get; set; }
    }
}