using MovieWebSite.Models;

namespace MovieWebSite.Services
{
    public class MovieService : IMovieService
    {
        // Filmleri döndüren metod
        public List<Movie> GetMovies()
        {
        return new List<Movie>
            {
      new Movie {MovieId = 1, Title = "X-Men: The Last Stand", Director = "Brett Ratner", Stars = new[] { "Patrick Stewart", "Hugh Jackman", "Halle Berry" }, ReleaseYear = 2006, ImageUrl="https://upload.wikimedia.org/wikipedia/tr/7/74/Spider-Man547.jpg"},
            new Movie {MovieId = 2, Title = "Spider Man 2", Director = "Sam Raimi", Stars = new[] { "Tobey Maguire", "Kirsten Dunst", "Alfred Molina" }, ReleaseYear = 2004, ImageUrl="https://upload.wikimedia.org/wikipedia/tr/7/74/Spider-Man547.jpg"},
            new Movie {MovieId = 3, Title = "Spider Man 3", Director = "Sam Raimi", Stars = new[] { "Tobey Maguire", "Kirsten Dunst", "Topher Grace" }, ReleaseYear = 2007, ImageUrl="https://upload.wikimedia.org/wikipedia/tr/7/74/Spider-Man547.jpg"},
            new Movie {MovieId = 4, Title = "Valkyrie", Director = "Bryan Singer", Stars = new[] { "Tom Cruise", "Bill Nighy", "Carice van Houten" }, ReleaseYear = 2008, ImageUrl = "https://upload.wikimedia.org/wikipedia/tr/7/74/Spider-Man547.jpg"},
            new Movie {MovieId = 5, Title = "Gladiator", Director = "Ridley Scott", Stars = new[] { "Russell Crowe", "Joaquin Phoenix", "Connie Nielsen" }, ReleaseYear = 2000, ImageUrl="https://upload.wikimedia.org/wikipedia/tr/7/74/Spider-Man547.jpg"}
            };
        }
    }
}
