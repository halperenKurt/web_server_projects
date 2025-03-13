namespace MovieWebSite.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string[] Stars { get; set; }  // Filmdeki oyuncuları tutan dizi
        public int ReleaseYear { get; set; }

        public string ImageUrl { get; set; }
    }
}
