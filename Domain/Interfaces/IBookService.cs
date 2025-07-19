using BookStoreAPI.Application.DTOs;
using BookStoreAPI.Domain.Entities;

namespace BookStoreAPI.Domain.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<BookDTO>> GetAllBooksAsync();
        public Task<BookDTO?> GetBookAsync(Guid Id);
        public Task<int> PostBookAsync(Book book);

        public Task<int> DeleteBookAsync(Guid Id);

        public Task<int> UpdateBookAsync(BookUpdateRequest book);

        public Task<IEnumerable<BookDTO>> GetTop10Books();


    }
}
