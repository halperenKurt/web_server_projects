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
    // Kullan�c� �erezlerini kontrol et
    string firstName = Request.Cookies["session_userFirstName"];
    string lastName = Request.Cookies["session_userLastName"];

    if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
    {
    ViewBag.WelcomeMessage = $"Welcome {firstName} {lastName}!";
    ViewBag.IsLoggedIn = true; // Kullan�c� giri� yapm��
    }
    else
    {
    ViewBag.WelcomeMessage = "Welcome, Please Login";
    ViewBag.IsLoggedIn = false; // Kullan�c� giri� yapmam��
    }

    // MovieService'ten film listesini al�yoruz
    var movies = _movieService.GetMovies(); // Burada movies null olabilir mi?

    if (movies == null || !movies.Any())
    {
    // E�er null veya bo�sa, hata verebiliriz
    ViewBag.Error = "Filmler al�n�rken bir hata olu�tu!";
    }

    return View(movies); // Filmleri View'a g�nderiyoruz
    }

    public IActionResult Privacy()
    {
    return View();
    }

    // Kullan�c� ��k��� yapacak (�erezleri temizle)
    public IActionResult Logout()
    {
    // �erezleri sil
    Response.Cookies.Delete("session_userFirstName");
    Response.Cookies.Delete("session_userLastName");

    // ��k�� yapt�ktan sonra ana sayfaya y�nlendir
    return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
