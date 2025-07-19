using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStoreAPI.Domain.Entities;
using BookStoreAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/[controller]")]
public class LoginController(IUserService userService) : ControllerBase
{
    

    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] User user )
    {
        var token = await userService.Authenticate(user);
        if(token == null) 
        {
            return Unauthorized();
        }

        return Ok(token);
    }
    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] User user)
    {
        if (await userService.Register(user))
        {
            return BadRequest("User already exists");
        }

        return Ok("User registered");
    }

}
