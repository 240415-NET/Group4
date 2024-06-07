using ActivityTracker.Models;
using Microsoft.AspNetCore.Mvc;
using ActivityTracker.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration.UserSecrets;
using Azure.Identity;

namespace ActivityTracker.Controllers;

[ApiController]
//[Route("users")]

public class ActivityController : ControllerBase
{
    private readonly IActivityService _activityService;
    public ActivityController (IActivityService activityServiceFromBuilder)
    {
        _activityService = activityServiceFromBuilder;
    }

    [HttpPost("/Activity")]
    public async Task<ActionResult> PostNewActivity(string newActivityDescriptionFromFrontEnd, DateOnly newActivityDateFromFrontEnd,
                                                    TimeOnly newActivityTimeFromFrontEnd,
                                                    string newActivityNameOfPersonFromFrontEnd,
                                                    string userNameToAddActivityToFromFrontEnd)
    {
        try
        {
            Activity newActivityToAdd = new Activity();
            newActivityToAdd.activity_Description = newActivityDescriptionFromFrontEnd;
            newActivityToAdd.Date_OfActivity = newActivityDateFromFrontEnd;
            newActivityToAdd.Time_OfActivity = newActivityTimeFromFrontEnd;
            newActivityToAdd.nameOfPerson = newActivityNameOfPersonFromFrontEnd;
            newActivityToAdd.user.userName = userNameToAddActivityToFromFrontEnd;

            await _activityService.CreateNewActivityAsync(newActivityToAdd);
            return Ok($"{newActivityToAdd.activity_Description} was created.");

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/Get")]
    public async Task<ActionResult<List<Activity>>> GetAllActivitiesByUserName(string username)
    {
        try
        {
            List<Activity> userActivitiesList = await _activityService.GetAllActivitiesByUserNameAsync(username);
            return Ok(userActivitiesList);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}
