using insuranceApp1.Models;

namespace insuranceApp1.Repositories
{
    public class ClaimRepository
    {
        InsuranceDbContext etx = null;
        public ClaimRepository(InsuranceDbContext etx)
        {
            this.etx = etx;
        }
        public bool AddClaim(Claim claim)
        {
            etx.Claims.Add(claim);
            int r = etx.SaveChanges();
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RemoveClaim(Claim claim)
        {
            etx.Claims.Remove(claim);
            int r = etx.SaveChanges();
            if (r > 0)
                return true;
            else
                return false;
        }
        public bool UpdateClaim(Claim claim)
        {
            Claim claimUp = etx.Claims.Find(claim.ClaimId);
            claimUp.ClaimAmount = claim.ClaimAmount;
            claimUp.ClaimStatus = claim.ClaimStatus;
            claimUp.SubmissionDate = claim.SubmissionDate;
            claimUp.SettlementDate = claim.SettlementDate;
            int r = etx.SaveChanges();
            if (r > 0)
                return true;
            else
                return false;

        }
        public Claim SearchClaim(int ClaimId)
        {
            return etx.Claims.Find(ClaimId);
        }
        public List<Claim> AllClaimDetails()
        {
            return etx.Claims.ToList();
        }
    }
}
