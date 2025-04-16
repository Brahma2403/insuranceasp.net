using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insuranceApp1.Models

{
    public class PremiumCalculation
    {
        [Key]
        public int CalculationId { get; set; }

        [ForeignKey("Policy")]
        public int PolicyId { get; set; }
        public virtual Policy Policy { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public double BasePremium { get; set; }

        public double AdjustedPremium { get; set; }
    }

}

