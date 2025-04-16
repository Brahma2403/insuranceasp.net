using insuranceApp1.Models;
using insuranceApp1.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace insuranceApp1.Controllers
{
    public class ClaimController : Controller
    {
        // GET: GameController1
        ClaimRepository claimRep = null;
        public ClaimController(InsuranceDbContext etx)
        {
            claimRep = new ClaimRepository(etx);
        }
        public ActionResult Index()
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            List<Claim> claimers = claimRep.AllClaimDetails();
            return View(claimers);
        }

        // GET: GameController1/Details/5
        public ActionResult Details(int id)
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            Claim claimers = claimRep.SearchClaim(id);
            return View(claimers);
        }

        // GET: GameController1/Create
        public ActionResult Create()
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            Claim claimers = new Claim();
            return View(claimers);
        }

        // POST: GameController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Claim model)
        {
            try
            {
                bool b = claimRep.AddClaim(model);
                if (b)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: GameController1/Edit/5
        public ActionResult Edit(int id)
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            Claim claimObj = claimRep.SearchClaim(id);
            return View(claimObj);
        }

        // POST: GameController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Claim claimers)
        {
            try
            {
                claimRep.UpdateClaim(claimers);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: GameController1/Delete/5
        public ActionResult Delete(int id)
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            Claim claimers = claimRep.SearchClaim(id);
            claimRep.RemoveClaim(claimers);
            return RedirectToAction("Index");
        }

        // POST: GameController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
