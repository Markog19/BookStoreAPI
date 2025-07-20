using BookStoreAPI.Application.DTOs;
using BookStoreAPI.Domain.Interfaces;
using BookStoreAPI.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookStoreApi.Tests.UnitTests;
public class BookServiceTests
{
    [Fact]
    public async Task GetAllBooks_Returns_AllBooksAsync()
    {
        
        var mockService = new Mock<IBookService>();
        var books = new List<BookDTO>
    {
        new BookDTO { Title = "Book 1", AuthorNames = new List<string> { "Author A" }, GenreNames = new List<string> { "Genre A" }, AverageRating = 5.0 },
        new BookDTO { Title = "Book 2", AuthorNames = new List<string> { "Author B" }, GenreNames = new List<string> { "Genre B" }, AverageRating = 4.0 }
    };

        mockService
            .Setup(s => s.GetAllBooksAsync())
            .ReturnsAsync(books.AsEnumerable());

        var controller = new BooksController(mockService.Object);

        var result = await controller.GetAsync();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedBooks = Assert.IsAssignableFrom<IEnumerable<BookDTO>>(okResult.Value);
        Assert.Equal(2, returnedBooks.Count());
    }
}
