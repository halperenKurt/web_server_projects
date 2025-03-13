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
                new Movie { MovieId = 1, Title = "X-Men: The Last Stand", Director = "Brett Ratner", ReleaseYear = 2006 },
                new Movie { MovieId = 2, Title = "Spider Man 2", Director = "Sam Raimi", ReleaseYear = 2004 },
                new Movie { MovieId = 3, Title = "Spider Man 3", Director = "Sam Raimi", ReleaseYear = 2007 },
                new Movie { MovieId = 4, Title = "Valkyrie", Director = "Bryan Singer", ReleaseYear = 2008 },
                new Movie { MovieId = 5, Title = "Gladiator", Director = "Ridley Scott", ReleaseYear = 2000 }
            };
        }
    }
}
