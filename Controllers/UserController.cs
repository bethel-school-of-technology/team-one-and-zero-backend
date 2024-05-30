using Microsoft.AspNetCore.Mvc;
using TEAM_ONE_AND_ZERO_BACKEND.Repositories;
using TEAM_ONE_AND_ZERO_BACKEND.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TEAM_ONE_AND_ZERO_BACKEND.Controllers;

[ApiController]

[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _authService;

    public UserController(ILogger<UserController> logger, IUserRepository service)
    {
        _logger = logger;
        _authService = service;
    }

    [HttpPost]
    [Route("register")]
    public ActionResult CreateUser(User user)
    {
        if (user == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        _authService.CreateUser(user);
        return NoContent();
    }

    [HttpGet]
    [Route("login")]
    public ActionResult<string> SignIn(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return BadRequest();
        }

        var token = _authService.SignIn(username, password);

        if(string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized();
        }

        return Ok(token);
    }

    [HttpGet]
    [Route("{userId:int}")]
    public ActionResult<User> GetUserById(int userId)
    {
        var user = _authService.GetUserById(userId);
        
        if(user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        return Ok(_authService.GetAllUsers());
    }

    [HttpGet]
    [Route("{username}")]
    public async Task<ActionResult<User>> GetUserByUsername(string username)
    {
        var name = _authService.GetUserByUsername(username);

        if (name == null)
        {
            return NotFound();
        }

        return Ok(await name);
    }

    [HttpGet]
    [Route("current")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult GetCurrentUser()
    {
        bool isLoggedIn = User.Identity.IsAuthenticated;

        if(!isLoggedIn)
        {
            return NotFound();

        }
        
        var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        User currentUser = _authService.GetUserById(id);

        return Ok(currentUser);
    }
}
