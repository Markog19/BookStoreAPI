using BookStoreAPI.Application.Services;
using BookStoreAPI.Domain.Entities;
using BookStoreAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController(IReviewService reviewService) : ControllerBase
{
    [Authorize(Roles = "Read, ReadWrite")]
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var reviews = await reviewService.GetAllAsync();
        return reviews.Any() ? Ok(reviews) : NotFound(reviews);
    }

    [Authorize(Roles = "Read, ReadWrite")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var review = await reviewService.GetByIdAsync(id);
        return review != null ? Ok(review) : NotFound(review);
    }

    [Authorize(Roles = "ReadWrite")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Review review)
    {
        var created = await reviewService.CreateAsync(review);
        if (created == 0)
        {
            return BadRequest("Post request failed");
        }
        return Ok("Success");
    }

    [Authorize(Roles = "ReadWrite")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] Review review)
    {
        var success = await reviewService.UpdateAsync(review);
        return success ? Ok("Success") : BadRequest("Update failed");
    }

    [Authorize(Roles = "ReadWrite")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await reviewService.DeleteAsync(id);
        return success ? Ok("Success") : BadRequest("Delete failed");
    }
}
