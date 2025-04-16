using insuranceApp1.Models;
using insuranceApp1.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace insuranceApp1.Controllers
{
    public class HomeController : Controller
    {
        UsersRepository userRep = null;
        CustomerRepository cusRep = null;

        public HomeController(InsuranceDbContext htx) //Dependency injection
        {
            userRep = new UsersRepository(htx);
            cusRep = new CustomerRepository(htx);
        }

        public IActionResult AdminWelcome()
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            return View();
        }

        public IActionResult UserWelcome()
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(IFormCollection ifrm)
        {
            string userName = ifrm["txUser"];
            string password = ifrm["txPsw"];
            string role = ifrm["Role"];
            bool b = userRep.IsValidUser(userName, password, role);
            if (b)
            {
                if (role == "User")
                {
                    HttpContext.Session.SetString("UserRole", "User");
                    HttpContext.Session.SetString("UserName", userName);
                    return RedirectToAction("UserWelcome","Home");
                }
                else if(role == "Admin")
                {
                    HttpContext.Session.SetString("UserRole", "Admin");
                    HttpContext.Session.SetString("UserName", userName);
                    return RedirectToAction("AdminWelcome","Home");
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Invalid Login Credentials";
                return View("HomeErrorView");
            }
        }

        
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(IFormCollection ifrm)
        {
            string userName = ifrm["txName"];
            string password = ifrm["txPsw"];
            string role = ifrm["Role"];
            string address = ifrm["txAddr"];
            string phoneno = ifrm["txPhone"];
            string email = ifrm["txEmail"];

            if (!userRep.IsUserExists(userName))
            {
                Customer c = new Customer()
                {
                    CustomerId=0,
                    Name = userName,
                    Address = address,
                    Email=email,
                    Phone=phoneno
                };
                cusRep.AddCustomer(c);
                userRep.AddUser(userName, password,role);
                ViewBag.Message = "Registered successfully!";
            }
            else
            {
                ViewBag.Message = "User already exists!";
            }
            return View();
        }
    }
}