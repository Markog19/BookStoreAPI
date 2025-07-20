namespace BookStoreAPI.Application.DTOs
{
    public class RawBookDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double AverageRating { get; set; }
    }
}
