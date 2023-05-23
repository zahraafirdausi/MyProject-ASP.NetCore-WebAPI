using Client.Models;
using Client.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        // Get Action
        public IActionResult Login()
        {

            if (HttpContext.Session.GetString("email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Departments");
            }
        }

        //Post Action
        [HttpPost]
        public ActionResult Login(string email)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                if (ModelState.IsValid) 
                {
                    HttpContext.Session.SetString("email", email.ToString());
                    return RedirectToAction("Index", "Departments");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Logout()
        {

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("email");

            return RedirectToAction("Login");
        }

        [Authentication]
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}