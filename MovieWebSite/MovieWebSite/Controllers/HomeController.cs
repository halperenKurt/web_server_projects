using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Models;
using System.Diagnostics;

namespace MovieWebSite.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
  
        public IActionResult Index()
        {
            string firstName = Request.Cookies["session_userFirstName"];
            string lastName = Request.Cookies["session_userLastName"];

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                ViewBag.WelcomeMessage = $"Welcome {firstName} {lastName}!";
                ViewBag.IsLoggedIn = true; // Kullanýcýnýn giriþ yaptýðýný belirtiyoruz
            }
            else
            {
                ViewBag.WelcomeMessage = "Welcome Please Login";
                ViewBag.IsLoggedIn = false;
            }
            return RedirectToAction("Index", "Movie");
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
