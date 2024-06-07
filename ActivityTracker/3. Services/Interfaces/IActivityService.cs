
using ActivityTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Services;
public interface IActivityService
{
    public Task<Activity> CreateNewActivityAsync(Activity newActivityToAddFromController);
    public Task<List<Activity>> GetAllActivitiesByUserNameAsync(string username);

}