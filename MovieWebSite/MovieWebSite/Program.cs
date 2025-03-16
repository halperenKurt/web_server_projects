using MovieWebSite.Services;  // Services namespace'ini eklemelisin
using Microsoft.AspNetCore.Session;  // Session için gerekli namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// IMovieService'i singleton olarak ekliyoruz
builder.Services.AddSingleton<IMovieService, MovieService>();  // IMovieService ve MovieService baðlantýsýný saðlýyoruz

// CartService'i singleton olarak ekliyoruz
builder.Services.AddSingleton<CartService>();  // CartService baðlantýsýný saðlýyoruz

// Session kullanýmý için gerekli servisleri ekliyoruz
builder.Services.AddDistributedMemoryCache();  // Memory cache kullanacaðýz
builder.Services.AddSession(options =>  // Session yapýlandýrmasýný buraya ekliyoruz
{
options.IdleTimeout = TimeSpan.FromMinutes(30);  // Session süresi
options.Cookie.HttpOnly = true;
options.Cookie.IsEssential = true;  // GDPR uyumluluðu
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
app.UseSession();  // Session'ý kullanabilmek için bu middleware'i ekliyoruz

app.UseAuthorization();  // Yetkilendirme middleware'i

// Default route için yönlendirme yapýyoruz
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
