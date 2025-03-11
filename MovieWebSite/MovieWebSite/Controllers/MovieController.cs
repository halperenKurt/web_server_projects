using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Models;

namespace MovieWebSite.Controllers
{
    public class MovieController : Controller
    {
        private static List<Movie> movies = new List<Movie>
        {
            new Movie {MovieId = 1, Title = "X-Men: The Last Stand", Director = "Brett Ratner",Stars=["Patrick Stewart", "Hugh Jackman","Halle Berry"],ReleaseYear=2006},
            new Movie {MovieId = 2, Title = "Spider Man 2", Director = "Sam Raimi",Stars=["Tobey Maguire", "Kirsten Dunst","Alfred Molina"],ReleaseYear=2004},
            new Movie {MovieId = 3, Title = "Spider Man 3", Director = "Sam Raimi",Stars=["Tobey Maguire", "Kirsten Dunst","Topher Grace"],ReleaseYear=2007},
            new Movie {MovieId = 4, Title = "Valkyrie", Director = "Brayn Singer",Stars=["Tom Cruise", "Bill Nighy","Carice van Houten"],ReleaseYear=2008},
            new Movie {MovieId = 5, Title = "Gladiator", Director = "Ridley Scott",Stars=["Russell Crowe", "Joaquin Phoneix","Connie Nielsen"],ReleaseYear=2000}
        };
        public ActionResult Index()
        {
            return View(movies);
        }


        public IActionResult Detail(int id)

        {
            var movie = movies.FirstOrDefault(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

    }
}
