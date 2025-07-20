using System.Xml.Linq;
using BookStoreAPI.Application.DTOs;
using BookStoreAPI.Domain.Entities;
using BookStoreAPI.Domain.Interfaces;
using BookStoreAPI.Infrastructure.Data;
using Humanizer;
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

        public async Task<IEnumerable<BookDTO>> GetTop10Books()
        {
           FormattableString booksSql= $@"
    SELECT b.""Title"", b.""Id"", CASE
        WHEN EXISTS (
            SELECT 1 FROM ""Reviews"" AS r WHERE b.""Id"" = r.""BookId"") THEN (
            SELECT AVG(r0.""Rating""::double precision)
            FROM ""Reviews"" AS r0 WHERE b.""Id"" = r0.""BookId"")
        ELSE 0.0
    END AS ""AverageRating""
    FROM ""Books"" AS b
    ORDER BY ""AverageRating"" DESC
    LIMIT 10;
";

            FormattableString authorSql = $@"
    SELECT s.""Name"" AS ""AuthorName"", b.""Id"" AS ""BookId""
    FROM ""Books"" AS b
    INNER JOIN (
        SELECT a1.""Name"", b2.""BookId""
        FROM ""BookAuthors"" AS b2
        INNER JOIN ""Authors"" AS a1 ON b2.""AuthorId"" = a1.""Id""
    ) AS s ON b.""Id"" = s.""BookId""
    ORDER BY b.""Id"";
";

            FormattableString genreSql = $@"
    SELECT s0.""Name"" AS ""GenreName"", b.""Id"" AS ""BookId""
    FROM ""Books"" AS b
    INNER JOIN (
        SELECT g0.""Name"", b4.""BookId""
        FROM ""BookGenres"" AS b4
        INNER JOIN ""Genres"" AS g0 ON b4.""GenreId"" = g0.""Id""
    ) AS s0 ON b.""Id"" = s0.""BookId""
    ORDER BY b.""Id"";
";
            var books = await context.Database.SqlQuery<RawBookDTO>(booksSql).ToListAsync();
            var authors = await context.Database.SqlQuery<RawAuthorDTO>(authorSql).ToListAsync();
            var genres = await context.Database.SqlQuery<RawGenreDTO>(genreSql).ToListAsync();

            var result = books.Select(book => new BookDTO
            {
                Title = book.Title,
                AverageRating = book.AverageRating,
                AuthorNames = authors
                    .Where(a => a.BookId == book.Id)
                    .Select(a => a.AuthorName)
                    .Distinct()
                    .ToList(),
                GenreNames = genres
                    .Where(g => g.BookId == book.Id)
                    .Select(g => g.GenreName)
                    .Distinct()
                    .ToList()
            }).ToList();

            return result;
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
