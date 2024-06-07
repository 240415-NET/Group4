using ActivityTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Data;
public interface IActivityStorageEFRepo
{
    public Task<Activity> CreateNewActivityInDBAsync (Activity newActivityToAddFromActivityService);
    public Task<List<Activity>> GetAllActivitiesbyUserNameFromDBAsync(string username);

}