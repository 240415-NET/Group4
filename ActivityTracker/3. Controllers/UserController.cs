using ActivityTracker.Models;
using Microsoft.AspNetCore.Mvc;
using ActivityTracker.Services;

namespace ActivityTracker.Controllers;

[ApiController]
[Route("users")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userServiceFromBuilder)
    {
        _userService = userServiceFromBuilder;
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostNewUser(string userNameFromFrontEnd, string userEmail = "", string firstName = "")
    {
        try
        {
            User newUserToCreate = new User(userNameFromFrontEnd);
            await _userService.CreateNewUserAsync(newUserToCreate);
            return Ok(newUserToCreate);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}