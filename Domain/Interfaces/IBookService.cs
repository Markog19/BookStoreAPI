using BookStoreAPI.Domain.Entities;

namespace BookStoreAPI.Domain.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<Book>> GetAllBooksAsync();
        public Task GetBookAsync(Guid Id);
        public Task PostBookAsync(Book book);

        public Task DeleteBookAsync(Guid Id);

        public Task UpdateBookAsync(Guid id, Book book);


        
    }
}
