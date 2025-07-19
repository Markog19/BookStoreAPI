using BookStoreAPI.Domain.Entities;
using BookStoreAPI.Domain.Interfaces;

namespace BookStoreAPI.Application.Services
{
    public class BookService : IBookService
    {
        public Task DeleteBookAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetBookAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task PostBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBookAsync(Guid id, Book book)
        {
            throw new NotImplementedException();
        }
    }
}
