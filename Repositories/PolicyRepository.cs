using insuranceApp1.Models;

namespace insuranceApp1.Repositories
{
    public class PolicyRepository
    {
        InsuranceDbContext ptx = null;
        public PolicyRepository(InsuranceDbContext ptx)
        {
            this.ptx = ptx;

        }
        public bool AddPolicy(Policy policy)
        {
            ptx.Policies.Add(policy);
            int r = ptx.SaveChanges();
            if (r > 0)
                return true;
            else
                return false;

        }
        public bool RemovePolicy(Policy policy)
        {
            ptx.Policies.Remove(policy);
            int r = ptx.SaveChanges();
            if (r > 0)
                return true;
            else
                return false;
        }
        public bool UpdatePolicy(Policy policy)
        {
            Policy avPolicy = ptx.Policies.Find(policy.PolicyId);
            avPolicy.PolicyType = policy.PolicyType;
            avPolicy.CoverageAmount = policy.CoverageAmount;
            avPolicy.PremiumAmount = policy.PremiumAmount;
            avPolicy.ValidityStartDate = policy.ValidityStartDate;
            avPolicy.ValidityEndDate = policy.ValidityEndDate;
            int r = ptx.SaveChanges();
            if (r > 0)
                return true;
            else
                return false;

        }
        public Policy SearchPolicy(int policyId)
        {
            return ptx.Policies.Find(policyId);
        }
        public List<Policy> AllPolicyDetails()
        {
            return ptx.Policies.ToList();
        }
    }
}

