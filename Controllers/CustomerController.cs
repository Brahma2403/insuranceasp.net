using insuranceApp1.Models;
using insuranceApp1.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace insuranceApp1.Controllers
{
    public class CustomerController : Controller
    {
        CustomerRepository cuRep = null;
        public CustomerController(InsuranceDbContext cutx) //Dependency injection feature
        {
            cuRep = new CustomerRepository(cutx);
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            List<Customer> customers = cuRep.AllCustomerDetails();
            return View(customers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            Customer customer = cuRep.SearchCustomer(id);
            return View(customer);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            Customer customer = new Customer();
            return View(customer);
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer model)
        {
            try
            {
                bool b = cuRep.AddCustomer(model);
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

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            Customer customerObj = cuRep.SearchCustomer(id);
            return View(customerObj);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                cuRep.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    Customer customer = cuRep.SearchCustomer(id);
        //    cuRep.RemoveCustomer(customer);
        //    return RedirectToAction("Index");
        //}

        // POST: CustomerController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}