using insuranceApp1.Models;
using insuranceApp1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace insuranceApp1.Controllers
{
    public class UserPremiumCalculationController : Controller
    {
        private readonly PremiumCalculationRepository _prrep;
        private readonly InsuranceDbContext _context;
        private Customer? customerObj;

        public UserPremiumCalculationController(InsuranceDbContext context)
        {
            _context = context;
            _prrep = new PremiumCalculationRepository(context);
        }
        // Display all premium calculations
        public ActionResult Index()
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            customerObj = _context.Customers.Where(c => c.Name == userName).FirstOrDefault();
            List<int> ids = _context.Calculations.Where(c => c.CustomerId == customerObj.CustomerId).Select(c => c.PolicyId).ToList();
            List<PremiumCalculation> cal = new List<PremiumCalculation>();
            foreach (int id in ids)
            {
                var ca = _context.Calculations.Find(id);
                if (ca != null)
                {
                    cal.Add(ca);
                }
            }
            return View(cal);
        }
    }
}