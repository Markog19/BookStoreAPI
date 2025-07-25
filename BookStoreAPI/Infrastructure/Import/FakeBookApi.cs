using BookStoreAPI.Application.DTOs;
using BookStoreAPI.Domain.Entities;
using FuzzySharp;
using Microsoft.Extensions.Options;
namespace BookStoreAPI.Infrastructure.Import;

public class FakeBookApi(IOptions<SeedOptions> seedOptions)
{ 
    public List<Book> GetBooksBatch(int count)
    {
        var books = new List<Book>();
        var random = new Random();

        for (int i = 1; i <= count; i++)
        {
            var title = $"{MockDataSources.TitleAdjectives[random.Next(MockDataSources.TitleAdjectives.Length)]} " +
                        $"{MockDataSources.TitleNouns[random.Next(MockDataSources.TitleNouns.Length)]}";

            if (!FindMatchingTitle(title,books, seedOptions.Value.FuzzScore))
            {
                books.Add(new Book
                {
                    Title = $"{title} #{i}",
                    Price = Math.Round((decimal)(random.NextDouble() * 50 + 5), 2),
                }); 
            }

        }

        return books;
    }
    public bool FindMatchingTitle(string importedTitle, IEnumerable<Book> existingBooks, int threshold = 90)
    {
        return existingBooks.Any(book => Fuzz.Ratio(importedTitle, book.Title) >= threshold);
    }
    public List<Author> GetAuthors(List<Book> books)
    {
        var authors = new List<Author>();
        var random = new Random();

        foreach (var book in books)
        {
            var authorName = $"{MockDataSources.AuthorFirstNames[random.Next(MockDataSources.AuthorFirstNames.Length)]} " +
                             $"{MockDataSources.AuthorLastNames[random.Next(MockDataSources.AuthorLastNames.Length)]}";
            authors.Add(new Author
            {
                Name = authorName,
                YearOfBirth = random.Next(1900, 2001)
            });
        }
        return authors;
    }
    public List<Genre> GetGenres()
    {
        var genres = new List<Genre>();

        foreach (var genre in MockDataSources.GenreNames)
        {
            genres.Add(new Genre
            {
                Name= genre,
            });
        }
        return genres;
    }
    public List<Review> GetReviews(List<Book> books)
    {
        var reviews = new List<Review>();
        var random = new Random();
        var numberOfReviews = random.Next(1, 4);
        foreach (var book in books)
        {
            for (int i = 0; i < numberOfReviews; i++)
            {
                reviews.Add(new Review
                {
                    BookId = book.Id,
                    Rating = random.Next(1, 6),
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu tortor nec eros" +
                    " venenatis facilisis at eu massa. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices " +
                    "posuere cubilia curae; Integer vitae lorem sit amet quam iaculis finibus. Maecenas quis est finibus, " +
                    "pretium lectus a, tincidunt diam. Cras eu neque leo. Phasellus magna dolor, eleifend ac imperdiet nec, " +
                    "accumsan eget lectus. Vestibulum mattis mattis rhoncus. Sed blandit varius libero, ac ultrices orci "
                });
            } 
        }
        return reviews;
    }

    
}
