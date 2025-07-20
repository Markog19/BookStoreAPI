namespace BookStoreAPI.Application.DTOs
{
    public class RawAuthorDTO
    {
        public Guid BookId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
    }
}
