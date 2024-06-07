using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ActivityTracker.Data;
using ActivityTracker.Models;
using ActivityTracker.Services;

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
    public async Task<ActionResult> PostNewActivity(ActivityDTO newActivity, string userName)
    {
        try
        {
            await _activityService.AddNewActivityAsync(newActivity, userName);

            return Ok("good job");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}