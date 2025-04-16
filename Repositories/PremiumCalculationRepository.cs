using insuranceApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace insuranceApp1.Repositories
{
    public class PremiumCalculationRepository
    {
        private readonly InsuranceDbContext _context;

        public PremiumCalculationRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        // Get all premium calculations
        public List<PremiumCalculation> AllCalculationDetails()
        {
            return _context.Calculations.Include(c => c.Policy).Include(c => c.Customer).ToList();
        }

        // Get calculation by Policy ID & Customer ID
        public PremiumCalculation GetCalculationByPolicyIdAndCustomerId(int policyId, int customerId)
        {
            return _context.Calculations.SingleOrDefault(c => c.PolicyId == policyId && c.CustomerId == customerId);
        }

        // Get policy details
        public Policy GetPolicyById(int policyId)
        {
            return _context.Policies.SingleOrDefault(p => p.PolicyId == policyId);
        }

        // Get customer details
        public Customer GetCustomerById(int customerId)
        {
            return _context.Customers.SingleOrDefault(c => c.CustomerId == customerId);
        }

        // Save Calculation (Insert or Update)
        public void SaveCalculation(int policyId, int customerId, double basePremium, double adjustedPremium)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingCalculation = GetCalculationByPolicyIdAndCustomerId(policyId, customerId);

                    if (existingCalculation != null)
                    {
                        existingCalculation.BasePremium = basePremium;
                        existingCalculation.AdjustedPremium = adjustedPremium;
                        _context.Calculations.Update(existingCalculation);
                    }
                    else
                    {
                        var newCalculation = new PremiumCalculation
                        {
                            PolicyId = policyId,
                            CustomerId = customerId,
                            BasePremium = basePremium,
                            AdjustedPremium = adjustedPremium
                        };
                        _context.Calculations.Add(newCalculation);
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Transaction failed: " + ex.Message);
                }
            }
        }
    }
}