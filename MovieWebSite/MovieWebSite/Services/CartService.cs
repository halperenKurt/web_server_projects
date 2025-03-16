using MovieWebSite.Models;

namespace MovieWebSite.Services
{
    public class CartService
    {
        private static List<Movie> cart = new List<Movie>();

        private readonly IMovieService _movieService;

        public CartService(IMovieService movieService)
        {
        _movieService = movieService;
        }

        public void AddToCart(int movieId)
        {
        var movie = _movieService.GetMovies().FirstOrDefault(m => m.MovieId == movieId);
        if (movie != null && !cart.Any(m => m.MovieId == movieId)) // Eğer film sepette yoksa ekle
        {
        cart.Add(movie);
        }
        }

        public List<Movie> GetCartItems()
        {
        return cart;
        }
    }
}
