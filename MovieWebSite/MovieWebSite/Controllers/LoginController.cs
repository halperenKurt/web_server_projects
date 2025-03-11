using Microsoft.AspNetCore.Mvc;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string userFirstName, string userLastName)
    {
        if (!string.IsNullOrEmpty(userFirstName) && !string.IsNullOrEmpty(userLastName))
        {
            // Cookie oluştur
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMonths(1), // Çerez 1 ay sürecek
                HttpOnly = true
            };

            Response.Cookies.Append("session_userFirstName", userFirstName, options);
            Response.Cookies.Append("session_userLastName", userLastName, options);

            return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendir
        }

        ViewBag.Error = "Lütfen ad ve soyad girin!";
        return View();
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("session_userFirstName");
        Response.Cookies.Delete("session_userLastName");

        return RedirectToAction("Index", "Home");
    }
}
