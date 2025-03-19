using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Models;
using MovieWebSite.Services;
using System.Diagnostics;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMovieService _movieService;

    public HomeController(ILogger<HomeController> logger, IMovieService movieService)
    {
    _logger = logger;
    _movieService = movieService;
    }

    public IActionResult Index()
    {
    
    string firstName = Request.Cookies["session_userFirstName"];
    string lastName = Request.Cookies["session_userLastName"];

    if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
    {
    ViewBag.WelcomeMessage = $"Welcome {firstName} {lastName}!";
    ViewBag.IsLoggedIn = true; 
    }
    else
    {
    ViewBag.WelcomeMessage = "Welcome, Please Login";
    ViewBag.IsLoggedIn = false; 
    }

    
    var movies = _movieService.GetMovies();
    if (movies == null || !movies.Any())
    {
  
    ViewBag.Error = "Filmler alýnýrken bir hata oluþtu!";
    }

    return View(movies);
    }

    public IActionResult Privacy()
    {
    return View();
    }

  
    public IActionResult Logout()
    {
    // Çerezleri sil
    Response.Cookies.Delete("session_userFirstName");
    Response.Cookies.Delete("session_userLastName");


    return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
