using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Models;
using MovieWebSite.Services;
using System.Text.Json;

namespace MovieWebSite.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly IMovieService _movieService;

        public CartController(CartService cartService, IMovieService movieService)
        {
            _cartService = cartService;
            _movieService = movieService;
        }

        public IActionResult Index()
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

            // Filmleri MovieService'ten al
            var movies = _movieService.GetMovies();

            if (movies == null || !movies.Any())
            {
                ViewBag.Error = "Filmler alınırken bir hata oluştu!";
            }

            // Session'dan sepet verisini al
            var cartJson = HttpContext.Session.GetString("Cart");
            var cartItems = string.IsNullOrEmpty(cartJson) ? new List<Movie>() : JsonSerializer.Deserialize<List<Movie>>(cartJson);

            return View(cartItems);
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
                    return Json(new { message = $"{movieToAdd.Title} sepete eklendi!" });
                }
                else
                {
                    return Json(new { message = "Bu film zaten sepette!" });
                }
            }
            return Json(new { message = "Film bulunamadı!" });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cartJson = HttpContext.Session.GetString("Cart");

            if (string.IsNullOrEmpty(cartJson))
            {
                return RedirectToAction("Index");
            }

            var cartItems = JsonSerializer.Deserialize<List<Movie>>(cartJson);
            var movieToRemove = cartItems.FirstOrDefault(m => m.MovieId == id);

            if (movieToRemove != null)
            {
                cartItems.Remove(movieToRemove);
                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));
            }

            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("session_userFirstName");
            Response.Cookies.Delete("session_userLastName");

            return RedirectToAction("Index");
        }
    }
}
