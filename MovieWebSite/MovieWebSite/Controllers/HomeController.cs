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
    // Kullanýcý çerezlerini kontrol et
    string firstName = Request.Cookies["session_userFirstName"];
    string lastName = Request.Cookies["session_userLastName"];

    if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
    {
    ViewBag.WelcomeMessage = $"Welcome {firstName} {lastName}!";
    ViewBag.IsLoggedIn = true; // Kullanýcý giriþ yapmýþ
    }
    else
    {
    ViewBag.WelcomeMessage = "Welcome, Please Login";
    ViewBag.IsLoggedIn = false; // Kullanýcý giriþ yapmamýþ
    }

    // MovieService'ten film listesini alýyoruz
    var movies = _movieService.GetMovies(); // Burada movies null olabilir mi?

    if (movies == null || !movies.Any())
    {
    // Eðer null veya boþsa, hata verebiliriz
    ViewBag.Error = "Filmler alýnýrken bir hata oluþtu!";
    }

    return View(movies); // Filmleri View'a gönderiyoruz
    }

    public IActionResult Privacy()
    {
    return View();
    }

    // Kullanýcý çýkýþý yapacak (çerezleri temizle)
    public IActionResult Logout()
    {
    // Çerezleri sil
    Response.Cookies.Delete("session_userFirstName");
    Response.Cookies.Delete("session_userLastName");

    // Çýkýþ yaptýktan sonra ana sayfaya yönlendir
    return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
