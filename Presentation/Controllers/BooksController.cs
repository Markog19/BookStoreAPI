using BookStoreAPI.Application.DTOs;
using BookStoreAPI.Domain.Entities;
using BookStoreAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BookStoreAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IBookService bookService) : ControllerBase
    {
        [Authorize(Roles = "Read, ReadWrite")]
        [HttpGet("allBooks")]
        public async Task<IActionResult> GetAsync()
        {
            var books = await bookService.GetAllBooksAsync();
            if (!books.Any())
            {
                return NotFound(books);

            }
            return Ok(books);
        }
        [Authorize(Roles = "Read, ReadWrite")]
        [HttpGet("top10")]
        public async Task<IActionResult> GetTop10Async()
        {
            var books = await bookService.GetTop10Books();
            if (!books.Any())
            {
                return NotFound(books);

            }
            return Ok(books);
        }

        [Authorize(Roles = "Read, ReadWrite")]
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(Guid id)
        {
            var book = await bookService.GetBookAsync(id);
            if (book == null)
            {
                return NotFound(book);

            }
            return Ok(book);
        }

        [Authorize(Roles = "ReadWrite")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            if(await bookService.PostBookAsync(book) == 0)
            {
                return BadRequest("Request failed");
            }
            return Ok("Sucess");

        }

        [Authorize(Roles = "ReadWrite")]
        [HttpPut("update")]
        public async Task<IActionResult> Put([FromBody] BookUpdateRequest book)
        {
            if (await bookService.UpdateBookAsync(book) == 0)
            {
                return BadRequest("Request failed");
            }
            return Ok("Success");
        }

        [Authorize(Roles = "ReadWrite")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            if (await bookService.DeleteBookAsync(id) == 0)
            {
                return BadRequest("Request failed");
            }
            return Ok("Success");
        }
    }
}
