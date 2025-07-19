using BookStoreAPI.Application.DTOs;
using BookStoreAPI.Domain.Entities;
using BookStoreAPI.Domain.Interfaces;
using BookStoreAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Application.Services
{
    public class BookService(DBContext context) : IBookService
    {
        public async Task<int> DeleteBookAsync(Guid Id)
        {
            if (!context.Books.Any(e => e.Id == Id))
            {

                return await Task.FromResult(0);
            }
            var fetchedBook = context.Books.FirstOrDefault(e => e.Id == Id);
            context.Remove(fetchedBook);
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
        {
            return await context.Books.
                Include(b => b.BookAuthors)
                .Include(b => b.BookGenres)
                .Include(b => b.Reviews)
                .AsSplitQuery()
                .Select(b => new BookDTO
                {
                    Title = b.Title,
                    AuthorNames = b.BookAuthors.Select(a => a.Author.Name).ToList(),
                    GenreNames = b.BookGenres.Select(g => g.Genre.Name).ToList(),
                    AverageRating = b.Reviews.Any() ? b.Reviews.Average(r => r.Rating) : 0
                })
                .ToListAsync(); 
        }

        public async Task<BookDTO?> GetBookAsync(Guid Id)
        {
            if (!context.Books.Any(e => e.Id == Id))
            {
                return null;
            }
            return await context.Books.Where(e => e.Id == Id)
                            .Include(b => b.BookAuthors)
                            .Include(b => b.BookGenres)
                            .Include(b => b.Reviews)
                            .AsSplitQuery()
                            .Select(b => new BookDTO
                            {
                                Title = b.Title,
                                AuthorNames = b.BookAuthors.Select(a => a.Author.Name).ToList(),
                                GenreNames = b.BookGenres.Select(g => g.Genre.Name).ToList(),
                                AverageRating = b.Reviews.Any() ? b.Reviews.Average(r => r.Rating) : 0
                            })
                            .FirstOrDefaultAsync();
        }

        public Task<IEnumerable<BookDTO>> GetTop10Books()
        {
            throw new NotImplementedException();
        }

        public async Task<int> PostBookAsync(Book book)
        {
            context.Add(book);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateBookAsync(BookUpdateRequest book)
        {
            if (!context.Books.Any(e => e.Id == book.Id))
            {

                return await Task.FromResult(0);
            }
            var fetchedBook = context.Books.FirstOrDefault(e => e.Id == book.Id);
            fetchedBook.Price = book.Price;
            return await context.SaveChangesAsync();
        }
    }
}
