using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Models;
using MovieWebSite.Services;
using System.Text.Json;

namespace MovieWebSite.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly IMovieService _movieService; // IMovieService'i enjekte ediyoruz

        public CartController(CartService cartService, IMovieService movieService)
        {
        _cartService = cartService;
        _movieService = movieService; // Bu satırı ekledik
        }

        public IActionResult Index()
        {
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

        // MovieService'ten film listesini alıyoruz
        var movies = _movieService.GetMovies(); // Burada movies null olabilir mi?

        if (movies == null || !movies.Any())
        {
        // Eğer null veya boşsa, hata verebiliriz
        ViewBag.Error = "Filmler alınırken bir hata oluştu!";
        }
        // Session'dan sepet verisini al
        var cartJson = HttpContext.Session.GetString("Cart");

        // Eğer sepet boşsa yeni bir liste oluştur
        var cartItems = string.IsNullOrEmpty(cartJson) ? new List<Movie>() : JsonSerializer.Deserialize<List<Movie>>(cartJson);

        return View(cartItems); // View'a cartItems listesini gönderiyoruz
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
        var cartJson = HttpContext.Session.GetString("Cart");

        // Eğer sepet boşsa yeni bir liste oluştur
        var cartItems = string.IsNullOrEmpty(cartJson) ? new List<Movie>() : JsonSerializer.Deserialize<List<Movie>>(cartJson);

        // Filme sepeti eklemeden önce var mı diye kontrol et
        var movieToAdd = _movieService.GetMovies().FirstOrDefault(m => m.MovieId == id); // MovieService'den filme ulaş

        if (movieToAdd != null)
        {
        // Film zaten sepette varsa ekleme
        if (!cartItems.Any(m => m.MovieId == movieToAdd.MovieId))
        {
        cartItems.Add(movieToAdd);
        // Güncel sepeti Session'a kaydet
        HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));

        // Sepete ekledikten sonra herhangi bir mesajı döndürebiliriz
        return Json(new { message = $"{movieToAdd.Title} sepete eklendi!" });
        }
        else
        {
        // Eğer film zaten sepette varsa bir uyarı mesajı döndür
        return Json(new { message = "Bu film zaten sepette!" });
        }
        }
        return Json(new { message = "Film bulunamadı!" });
        }

        [HttpPost]
        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
        var cartJson = HttpContext.Session.GetString("Cart");

        if (string.IsNullOrEmpty(cartJson))
        {
        return RedirectToAction("Index");
        }

        // Sepeti deseralize et
        var cartItems = JsonSerializer.Deserialize<List<Movie>>(cartJson);

        // Filmi sepetten çıkar
        var movieToRemove = cartItems.FirstOrDefault(m => m.MovieId == id);
        if (movieToRemove != null)
        {
        cartItems.Remove(movieToRemove);
        // Güncel sepeti tekrar Session'a kaydet
        HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));
        }

        // Sepet sayfasına geri dön
        return RedirectToAction("Index");
        }


       



    }
}
