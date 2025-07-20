using BookStoreAPI.Domain.Entities;
using BookStoreAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class GenresController(IGenreService genreService) : ControllerBase
{
    [Authorize(Roles = "Read, ReadWrite")]
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var genres = await genreService.GetAllAsync();
        return genres.Any() ? Ok(genres) : NotFound(genres);
    }

    [Authorize(Roles = "Read, ReadWrite")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var genre = await genreService.GetByIdAsync(id);
        return genre != null ? Ok(genre) : NotFound(genre);
    }

    [Authorize(Roles = "ReadWrite")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Genre genre)
    {
        var created = await genreService.CreateAsync(genre);
        if (created == 0)
        {
            return BadRequest("Post request failed");

        }
        return Ok("Success");
    }

    [Authorize(Roles = "ReadWrite")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] Genre genre)
    {
        var success = await genreService.UpdateAsync(genre);
        return success ? Ok("Success") : BadRequest("Update failed");
    }

    [Authorize(Roles = "ReadWrite")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await genreService.DeleteAsync(id);
        return success ? Ok("Success") : BadRequest("Delete failed");
    }
}