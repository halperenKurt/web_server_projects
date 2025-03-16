using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Models;
using MovieWebSite.Services;
using System.Collections.Generic;
using System.Linq;

namespace MovieWebSite.Controllers
{
    public class MovieController : Controller
    {
        // Filmler için örnek veri
        private static List<Movie> movies = new List<Movie>
        {
            new Movie {MovieId = 1, Title = "X-Men: The Last Stand", Director = "Brett Ratner", Stars = new[] { "Patrick Stewart", "Hugh Jackman", "Halle Berry" }, ReleaseYear = 2006, ImageUrl="https://upload.wikimedia.org/wikipedia/tr/7/74/Spider-Man547.jpg"},
            new Movie {MovieId = 2, Title = "Spider Man 2", Director = "Sam Raimi", Stars = new[] { "Tobey Maguire", "Kirsten Dunst", "Alfred Molina" }, ReleaseYear = 2004, ImageUrl="https://upload.wikimedia.org/wikipedia/tr/7/74/Spider-Man547.jpg"},
            new Movie {MovieId = 3, Title = "Spider Man 3", Director = "Sam Raimi", Stars = new[] { "Tobey Maguire", "Kirsten Dunst", "Topher Grace" }, ReleaseYear = 2007, ImageUrl="https://upload.wikimedia.org/wikipedia/tr/7/74/Spider-Man547.jpg"},
            new Movie {MovieId = 4, Title = "Valkyrie", Director = "Bryan Singer", Stars = new[] { "Tom Cruise", "Bill Nighy", "Carice van Houten" }, ReleaseYear = 2008, ImageUrl = "https://upload.wikimedia.org/wikipedia/tr/7/74/Spider-Man547.jpg"},
            new Movie {MovieId = 5, Title = "Gladiator", Director = "Ridley Scott", Stars = new[] { "Russell Crowe", "Joaquin Phoenix", "Connie Nielsen" }, ReleaseYear = 2000, ImageUrl="https://upload.wikimedia.org/wikipedia/tr/7/74/Spider-Man547.jpg"}
        };
        private readonly IMovieService _movieService;

        // Constructor with dependency injection
        public MovieController(IMovieService movieService)
        {
        _movieService = movieService;
        }

        // Film listesini gösteren metod
        public IActionResult Index()
        {
        // Use HttpContext to get cookies
        string firstName = Request.Cookies["session_userFirstName"];
        string lastName = Request.Cookies["session_userLastName"];

        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
        {
        ViewBag.WelcomeMessage = $"Welcome {firstName} {lastName}!";
        ViewBag.IsLoggedIn = true; // Kullanıcı giriş yapmış
        }
        else
        {
        ViewBag.WelcomeMessage = "Welcome, Please Login";
        ViewBag.IsLoggedIn = false; // Kullanıcı giriş yapmamış
        }

        return View();
       
        }

        // Film detaylarını gösteren metod
        public IActionResult Detail(int id)
        {
        

        // Film detaylarını getir
        var movie = movies.FirstOrDefault(m => m.MovieId == id);  // ID'ye göre filmi buluyoruz
        if (movie == null)
        {
        return NotFound();  // Film bulunamazsa 404 döndürüyoruz
        }

        return View(movie);  // Film detaylarını view'a gönderiyoruz
        }

    }
}
