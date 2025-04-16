using insuranceApp1.Models;
using insuranceApp1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace insuranceApp1.Controllers
{
    public class PremiumCalculationController : Controller
    {
        private readonly PremiumCalculationRepository _prrep;
        private readonly InsuranceDbContext _context;
        private Customer? customerObj;

        public PremiumCalculationController(InsuranceDbContext context)
        {
            _context = context;
            _prrep = new PremiumCalculationRepository(context);
        }

        // Show form for entering Policy ID & Customer ID
        public IActionResult FillCalculationTable()
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            return View();
        }

        // Display all premium calculations
        public ActionResult Index()
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            var premiumCalculations = _prrep.AllCalculationDetails();
            return View(premiumCalculations);
        }

        // Process user input & save calculation in the database
        [HttpPost]
        public IActionResult FillCalculationTable(int policyId, int customerId, double basePremium)
        {
            try
            {
                if (policyId <= 0 || customerId <= 0 || basePremium <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Invalid input values. Please enter correct details.");
                    return View();
                }

                var policy = _prrep.GetPolicyById(policyId);
                var customer = _prrep.GetCustomerById(customerId);

                if (policy == null || customer == null)
                {
                    ModelState.AddModelError(string.Empty, "Policy or Customer not found. Please check your input.");
                    return View();
                }

                var adjustedPremium = 1.1 * basePremium;
                _prrep.SaveCalculation(policyId, customerId, basePremium, adjustedPremium);

                ViewBag.PolicyId = policyId;
                ViewBag.CustomerId = customerId;
                ViewBag.BasePremium = basePremium;
                ViewBag.AdjustedPremium = adjustedPremium;
                TempData["Success"] = "Premium calculation saved successfully!";

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return View();
            }
        }
    }
}