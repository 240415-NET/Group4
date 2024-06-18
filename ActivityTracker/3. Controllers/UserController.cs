using ActivityTracker.Models;
using Microsoft.AspNetCore.Mvc;
using ActivityTracker.Services;
using ActivityTracker.Data;

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

    public async Task<ActionResult<User>> PostNewUser(string userNameFromFrontEnd, string userEmail = "", string firstName = "", string lastName = "")
    {
        try
        {

            User newUserToCreate = new User(userNameFromFrontEnd, userEmail, firstName, lastName);
            await _userService.CreateNewUserAsync(newUserToCreate);
            return Ok(newUserToCreate);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetUserByUserName(string userNameToFindFromFrontEnd)
    {
        try
        {
            User foundUser = await _userService.GetUserByUserNameAsync(userNameToFindFromFrontEnd);
            if (foundUser == null)
            {
                return NotFound($"Username {userNameToFindFromFrontEnd} was not found in the database.");
            }
            return Ok(foundUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<string>> DeleteUser(string userName)
    {
        try
        {
            string result = await _userService.DeleteUserAsync(userName);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }    
    }

    [HttpPatch]

    public async Task<ActionResult<UpdateUsernameDTO>> UpdateUserByUserName(UpdateUsernameDTO usernameToSwap)
    {
        try{
            //UpdateUsernameDTO user = usernameToSwap;
            await _userService.UpdateUsernameAsync(usernameToSwap);
            return Ok ($"{usernameToSwap.oldUserName}'s has been changed to:{usernameToSwap.newUserName}.");
        }
        catch(Exception e)
        {
            return  BadRequest(e.Message);
        }
    }



}