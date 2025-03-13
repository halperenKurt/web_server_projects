using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieWebSite.Controllers
{
    public class MovieController : Controller
    {
        // Filmler için örnek veri
        private static List<Movie> movies = new List<Movie>
        {
            new Movie {MovieId = 1, Title = "X-Men: The Last Stand", Director = "Brett Ratner", Stars = new[] { "Patrick Stewart", "Hugh Jackman", "Halle Berry" }, ReleaseYear = 2006, ImageUrl="https://www.playstation.com/tr-tr/games/marvels-spider-man-remastered/"},
            new Movie {MovieId = 2, Title = "Spider Man 2", Director = "Sam Raimi", Stars = new[] { "Tobey Maguire", "Kirsten Dunst", "Alfred Molina" }, ReleaseYear = 2004, ImageUrl="https://www.playstation.com/tr-tr/games/marvels-spider-man-remastered/"},
            new Movie {MovieId = 3, Title = "Spider Man 3", Director = "Sam Raimi", Stars = new[] { "Tobey Maguire", "Kirsten Dunst", "Topher Grace" }, ReleaseYear = 2007, ImageUrl="https://www.playstation.com/tr-tr/games/marvels-spider-man-remastered/"},
            new Movie {MovieId = 4, Title = "Valkyrie", Director = "Bryan Singer", Stars = new[] { "Tom Cruise", "Bill Nighy", "Carice van Houten" }, ReleaseYear = 2008, ImageUrl = "https://www.playstation.com/tr-tr/games/marvels-spider-man-remastered/"},
            new Movie {MovieId = 5, Title = "Gladiator", Director = "Ridley Scott", Stars = new[] { "Russell Crowe", "Joaquin Phoenix", "Connie Nielsen" }, ReleaseYear = 2000, ImageUrl="https://www.playstation.com/tr-tr/games/marvels-spider-man-remastered/"}
        };

        // Film listesini gösteren metod
        public IActionResult Index()
        {
        return View(movies);  // movies listesini view'a gönderiyoruz
        }

        // Film detaylarını gösteren metod
        public IActionResult Detail(int id)
        {
        var movie = movies.FirstOrDefault(m => m.MovieId == id);  // ID'ye göre filmi buluyoruz
        if (movie == null)
        {
        return NotFound();  // Film bulunamazsa 404 döndürüyoruz
        }
        return View(movie);  // Film detaylarını view'a gönderiyoruz
        }
    }
}
