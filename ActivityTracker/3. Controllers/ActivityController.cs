using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ActivityTracker.Data;
using ActivityTracker.Models;
using ActivityTracker.Services;
using Azure.Identity;

namespace ActivityTracker.Controllers;

[ApiController]
[Route("[controller]")]

public class ActivityController : ControllerBase
{
    public readonly IActivityService _activityService;
    public ActivityController(IActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpPost("/Activity {userName}")]
    public async Task<ActionResult<ActivityDTO>> PostNewActivity(ActivityDTO newActivity, string userName)
    {
        try
        {
            ActivityDTO activity = await _activityService.AddNewActivityAsync(newActivity, userName);
            // when we have a Get method, we may want to use that here (After the creation of the Activity)
            // that way, we can get the object and then return the activity details rather than "good job" :)

            return Ok(activity);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

        [HttpGet("/GetActivitiesbyUserName/{userName}")]
    public async Task<ActionResult<List<ActivityDTO>>> GetAllItemsForAUser(string userName)
    {
        try
        {
            //Creating a list to eventually *hopefully* return to our front end
            List<ActivityDTO> activitiesFound = await _activityService.GetAllActivitiesbyUserNameAsync(userName);

            return Ok(activitiesFound);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("/DeleteActivityByActivityName {userName}")]
    public async Task<ActionResult> DeleteActivityByActivityName(string activityDescriptionToDelete, string userName)
    {

        await _activityService.DeleteActivityByActivityNameAsync(activityDescriptionToDelete, userName);
        return Ok($"{activityDescriptionToDelete} has been deleted.");
    }

    [HttpDelete("/DeleteActivityByActivityId")]
    public async Task<ActionResult> DeleteActivityByActivityId(Guid activityIdToDelete)
    {

        string activityDescriptionToDelete = await _activityService.DeleteActivityByActivityIdAsync(activityIdToDelete);
        return Ok($"Activity Id: {activityIdToDelete} Activity Description: {activityDescriptionToDelete} has been deleted.");
    }

    [HttpPatch("/UpdateActivityByActivityId")]
    public async Task<ActionResult> UpdateActivityByActivityId(Guid activityIdToUpdateFromFrontEnd)
    {
        try
        {
            string activityDescriptionToUpdate = await _activityService.UpdateActivityByActivityIdAsync(activityIdToUpdateFromFrontEnd);
            return Ok($"Activity Id \"{activityIdToUpdateFromFrontEnd}\", Activity Description \"{activityDescriptionToUpdate}\" has been updated.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}