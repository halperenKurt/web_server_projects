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
            new Movie {MovieId = 1, Title = "X-Men: The Last Stand", Director = "Brett Ratner", Stars = new[] { "Patrick Stewart", "Hugh Jackman", "Halle Berry" }, ReleaseYear = 2006, ImageUrl="https://m.media-amazon.com/images/M/MV5BMThmOWE3OWEtODJmNC00ZDEzLTk4MWUtNzEzM2RiNmJiZmU3XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg"},
            new Movie {MovieId = 2, Title = "Spider Man 2", Director = "Sam Raimi", Stars = new[] { "Tobey Maguire", "Kirsten Dunst", "Alfred Molina" }, ReleaseYear = 2004, ImageUrl="https://tr.web.img2.acsta.net/pictures/bzp/01/46112.jpg"},
            new Movie {MovieId = 3, Title = "Spider Man 3", Director = "Sam Raimi", Stars = new[] { "Tobey Maguire", "Kirsten Dunst", "Topher Grace" }, ReleaseYear = 2007, ImageUrl="https://m.media-amazon.com/images/S/pv-target-images/2d7b36c419b03a1f82ee0c543c0e629a5dc690a567217d543afb4d12dd62b302.jpg"},
            new Movie {MovieId = 4, Title = "Valkyrie", Director = "Bryan Singer", Stars = new[] { "Tom Cruise", "Bill Nighy", "Carice van Houten" }, ReleaseYear = 2008, ImageUrl = "https://photo-vartabi.mncdn.com/mnresize/1440/-/product2/20250221050805-3e97a1e2-e09b-4b9f-b349-4c3cc1b1973b.jpg"},
            new Movie {MovieId = 5, Title = "Gladiator", Director = "Ridley Scott", Stars = new[] { "Russell Crowe", "Joaquin Phoenix", "Connie Nielsen" }, ReleaseYear = 2000, ImageUrl="https://tr.web.img3.acsta.net/pictures/bzp/01/24944.jpg"}
            };
        }
    }
}
