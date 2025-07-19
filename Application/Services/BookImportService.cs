using BookStoreAPI.Application.DTOs;
using BookStoreAPI.Domain.Entities;
using BookStoreAPI.Domain.Interfaces;
using BookStoreAPI.Infrastructure.Data;
using BookStoreAPI.Infrastructure.Import;
using FuzzySharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BookStoreAPI.Application.Services
{
    public class BookImportService(DBContext context, ILogger<BookImportService> _logger, IOptions<SeedOptions> seedOptions) : IBookImport
    {
        public async Task ImportAsync()
        {
            try
            {
                var fakeBookApi = new FakeBookApi(seedOptions);
                var books = fakeBookApi.GetBooksBatch(seedOptions.Value.DefaultBookCount.Value);
                var authors = fakeBookApi.GetAuthors(books);
                var genres = fakeBookApi.GetGenres();
                var reviews = fakeBookApi.GetReviews(books);
                var bookAuthors = new List<BookAuthor>();
                var bookGenres = new List<BookGenre>();

                 
                foreach (var book in books)
                {
                    var bookAuthor = CreateBookAuthor(book, authors);
                    if  ((await context.BookAuthors.FindAsync(bookAuthor.BookId, bookAuthor.AuthorId)) == null)
                    {
                        bookAuthors.Add(bookAuthor);

                    }
                    var bookGenre = CreateBookGenre(book, genres);
                    if ((await context.BookGenres.FindAsync(bookGenre.BookId, bookGenre.GenreId)) == null)
                    {
                        bookGenres.Add(bookGenre);
                    }
                }

                context.AddRange(books);
                context.AddRange(genres);
                context.AddRange(authors);
                context.AddRange(reviews);
                context.AddRange(bookGenres);
                context.AddRange(bookAuthors);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private BookAuthor CreateBookAuthor(Book book, List<Author> authors)
        {
            var authorsCount = authors.Count();
            var authorsIndex = new Random().Next(0, authorsCount);
            var randomAuthor = authors
                .Skip(authorsIndex)
                .FirstOrDefault();
            var bookAuthor = new BookAuthor
            {
                BookId = book.Id,
                AuthorId = randomAuthor.Id,
            };
            return bookAuthor;
        }

        private BookGenre CreateBookGenre(Book book, List<Genre> genres)
        {
            var genresCount = genres.Count();
            var genresIndex = new Random().Next(0, genresCount);
            var randomGenre = genres
                .Skip(genresIndex)
                .FirstOrDefault();
            var bookGenre = new BookGenre
            {
                BookId = book.Id,
                GenreId = randomGenre.Id,
            };
            return bookGenre;
        }
        
    
    
    }
    
}
    
