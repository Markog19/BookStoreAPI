using BookStoreAPI.Domain.Entities;
using BookStoreAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController(IAuthorService authorService) : ControllerBase
    {
        
            [Authorize(Roles = "Read, ReadWrite")]
            [HttpGet]
            public async Task<IActionResult> GetAsync()
            {
                var authors = await authorService.GetAllAsync();
                return authors.Any() ? Ok(authors) : NotFound(authors);
            }

            [Authorize(Roles = "Read, ReadWrite")]
            [HttpGet("{id}")]
            public async Task<IActionResult> Get(Guid id)
            {
                var author = await authorService.GetByIdAsync(id);
                return author != null ? Ok(author) : NotFound(author);
            }

            [Authorize(Roles = "ReadWrite")]
            [HttpPost]
            public async Task<IActionResult> Post([FromBody] Author author)
            {
                var created = await authorService.CreateAsync(author);
                if(created == 0)
                {
                return BadRequest("Post request failed");
                }
                return Ok("Success");
            }

            [Authorize(Roles = "ReadWrite")]
            [HttpPut("{id}")]
            public async Task<IActionResult> Put([FromBody] Author author)
            {
                var success = await authorService.UpdateAsync(author);
                return success ? Ok("Success") : BadRequest("Update failed");
            }

            [Authorize(Roles = "ReadWrite")]
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                var success = await authorService.DeleteAsync(id);
                return success ? Ok("Success") : BadRequest("Delete failed");
            }
        }
}
