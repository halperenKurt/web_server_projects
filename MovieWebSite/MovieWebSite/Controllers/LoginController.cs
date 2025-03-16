using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

public class LoginController : Controller
{
    // GET methodu, login sayfasını gösterir
    [HttpGet]
    public IActionResult Login()
    {
    return View();
    }

    // POST methodu, kullanıcıyı giriş yapmaya yönlendirir
    [HttpPost]
    public IActionResult Login(string userFirstName, string userLastName)
    {
    // Ad ve soyad boş değilse
    if (!string.IsNullOrWhiteSpace(userFirstName) && !string.IsNullOrWhiteSpace(userLastName))
    {
    // Cookie ayarları
    var cookieOptions = new CookieOptions
    {
        Expires = DateTime.UtcNow.AddMonths(1),  // Çerez 1 ay boyunca geçerli
        HttpOnly = true,                         // XSS saldırılarına karşı koruma
        Secure = false,               // HTTPS üzerinden çalışmasını sağlar (Geliştirme ortamında HTTP olabilir)
        SameSite = SameSiteMode.Strict           // CSRF koruması için
    };

    // Kullanıcı bilgilerini çerez olarak kaydet
    Response.Cookies.Append("session_userFirstName", userFirstName, cookieOptions);
    Response.Cookies.Append("session_userLastName", userLastName, cookieOptions);

    // Giriş yaptıktan sonra, ana sayfaya yönlendir
    return RedirectToAction("Index", "Home");
    }

    // Eğer kullanıcı adı ya da soyadı girilmemişse, hata mesajı göster
    ViewBag.Error = "Lütfen ad ve soyad girin!";
    return View();  // Hata mesajıyla birlikte login sayfasını tekrar göster
    }

    // Logout (Çıkış) işlemi
    [HttpPost]
    public IActionResult Logout()
    {
    // Kullanıcıya ait çerezleri sil
    Response.Cookies.Delete("session_userFirstName");
    Response.Cookies.Delete("session_userLastName");

    // Çıkış yaptıktan sonra Login sayfasına yönlendir
    return RedirectToAction("Login", "Login");
    }
}
