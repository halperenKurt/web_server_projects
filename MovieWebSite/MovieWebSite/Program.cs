using MovieWebSite.Services;  // Services namespace'ini eklemelisin
using Microsoft.AspNetCore.Session;  // Session i�in gerekli namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// IMovieService'i singleton olarak ekliyoruz
builder.Services.AddSingleton<IMovieService, MovieService>();  // IMovieService ve MovieService ba�lant�s�n� sa�l�yoruz

// CartService'i singleton olarak ekliyoruz
builder.Services.AddSingleton<CartService>();  // CartService ba�lant�s�n� sa�l�yoruz

// Session kullan�m� i�in gerekli servisleri ekliyoruz
builder.Services.AddDistributedMemoryCache();  // Memory cache kullanaca��z
builder.Services.AddSession(options =>  // Session yap�land�rmas�n� buraya ekliyoruz
{
options.IdleTimeout = TimeSpan.FromMinutes(30);  // Session s�resi
options.Cookie.HttpOnly = true;
options.Cookie.IsEssential = true;  // GDPR uyumlulu�u
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
app.UseExceptionHandler("/Home/Error");
app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Session middleware'ini ekliyoruz
app.UseSession();  // Session'� kullanabilmek i�in bu middleware'i ekliyoruz

app.UseAuthorization();  // Yetkilendirme middleware'i

// Default route i�in y�nlendirme yap�yoruz
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
