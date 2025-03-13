using MovieWebSite.Models;

namespace MovieWebSite.Services
{
    public interface IMovieService
    {
        List<Movie> GetMovies(); // Film listesi döndüren metod
    }
}