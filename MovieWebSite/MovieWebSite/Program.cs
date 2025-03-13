using MovieWebSite.Services;  // Services namespace'ini eklemelisin

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Burada IMovieService'i singleton olarak ekliyoruz
builder.Services.AddSingleton<IMovieService, MovieService>();  // IMovieService ve MovieService baðlantýsýný saðlýyoruz

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
