using insuranceApp1.Models;
using insuranceApp1.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace insuranceApp1.Controllers
{
    public class PolicyController : Controller
    {
        // GET: PolicyController
        PolicyRepository policyRep = null;
        public PolicyController(InsuranceDbContext ptx)
        {
            policyRep = new PolicyRepository(ptx);
        }
        public ActionResult Index()
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            List<Policy> policies = policyRep.AllPolicyDetails();
            return View(policies);
        }

        // GET: GameController1/Details/5
        public ActionResult Details(int id)
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            Policy policy = policyRep.SearchPolicy(id);
            return View(policy);
        }

        // GET: GameController1/Create
        public ActionResult Create()
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            Policy policy = new Policy();
            return View(policy);
        }

        // POST: GameController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Policy model)
        {
            try
            {
                bool b = policyRep.AddPolicy(model);
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
            Policy policyObj = policyRep.SearchPolicy(id);
            return View(policyObj);
        }

        // POST: GameController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Policy policy)
        {
            try
            {
                policyRep.UpdatePolicy(policy);
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
            Policy policy = policyRep.SearchPolicy(id);
            policyRep.RemovePolicy(policy);
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