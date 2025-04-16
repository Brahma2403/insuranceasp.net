using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace insuranceApp1.Models
{
    public class InsuranceDbContext:DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Users> Usersall { get; set; }
        public virtual DbSet<PremiumCalculation> Calculations { get; set; }
    }
}
