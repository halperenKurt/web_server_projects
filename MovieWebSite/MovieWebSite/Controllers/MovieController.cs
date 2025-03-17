using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Models;
using MovieWebSite.Services;
using System.Text.Json;

namespace MovieWebSite.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Detail(int id)
        {
            // Kullanıcı çerezlerini kontrol et
            string firstName = Request.Cookies["session_userFirstName"];
            string lastName = Request.Cookies["session_userLastName"];

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                ViewBag.WelcomeMessage = $"Welcome {firstName} {lastName}!";
                ViewBag.IsLoggedIn = true;
            }
            else
            {
                ViewBag.WelcomeMessage = "Welcome, Please Login";
                ViewBag.IsLoggedIn = false;
            }

            // Filmi ID'ye göre al
            var movie = _movieService.GetMovies().FirstOrDefault(m => m.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            // Sepet oturumunu kontrol et
            var cartJson = HttpContext.Session.GetString("Cart");
            var cartItems = string.IsNullOrEmpty(cartJson) ? new List<Movie>() : JsonSerializer.Deserialize<List<Movie>>(cartJson);

            // Film sepette mi kontrol et
            ViewBag.IsInCart = cartItems.Any(m => m.MovieId == id);

            return View(movie);
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            var cartItems = string.IsNullOrEmpty(cartJson) ? new List<Movie>() : JsonSerializer.Deserialize<List<Movie>>(cartJson);

            var movieToAdd = _movieService.GetMovies().FirstOrDefault(m => m.MovieId == id);

            if (movieToAdd != null)
            {
                if (!cartItems.Any(m => m.MovieId == movieToAdd.MovieId))
                {
                    cartItems.Add(movieToAdd);
                    HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));
                    return Json(new { message = $"{movieToAdd.Title} sepete eklendi!", success = true });
                }
                else
                {
                    return Json(new { message = "Bu film zaten sepette!", success = false });
                }
            }
            return Json(new { message = "Film bulunamadı!", success = false });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cartJson = HttpContext.Session.GetString("Cart");

            if (string.IsNullOrEmpty(cartJson))
            {
                return Json(new { message = "Sepet zaten boş!", success = false });
            }

            var cartItems = JsonSerializer.Deserialize<List<Movie>>(cartJson);
            var movieToRemove = cartItems.FirstOrDefault(m => m.MovieId == id);

            if (movieToRemove != null)
            {
                cartItems.Remove(movieToRemove);
                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));
                return Json(new { message = $"{movieToRemove.Title} sepetten çıkarıldı!", success = true });
            }

            return Json(new { message = "Film bulunamadı!", success = false });
        }
    }
}
