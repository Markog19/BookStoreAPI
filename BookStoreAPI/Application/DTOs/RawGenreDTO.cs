namespace BookStoreAPI.Application.DTOs
{
    public class RawGenreDTO
    {
        public Guid BookId { get; set; }
        public string GenreName { get; set; } = string.Empty;
    }
}
