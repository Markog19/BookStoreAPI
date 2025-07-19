namespace BookStoreAPI.Application.DTOs
{
    public class BookUpdateRequest
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }
    }
}
