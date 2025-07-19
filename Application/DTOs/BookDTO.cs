namespace BookStoreAPI.Application.DTOs
{
    public class BookDTO
    {
        public string Title { get; set; } = string.Empty;
        public List<string> AuthorNames { get; set; } = new();
        public List<string> GenreNames { get; set; } = new();
        public double AverageRating { get; set; }
    }
}
