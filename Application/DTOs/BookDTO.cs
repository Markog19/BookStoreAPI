using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Application.DTOs
{
    [Keyless]
    public class BookDTO
    {
        public string Title { get; set; } = string.Empty;
        public List<string> AuthorNames { get; set; } = new();
        public List<string> GenreNames { get; set; } = new();
        public double AverageRating { get; set; }
    }
}
