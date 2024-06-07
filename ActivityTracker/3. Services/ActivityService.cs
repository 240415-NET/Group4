using ActivityTracker.Models;
using ActivityTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ActivityTracker.Services;

public class ActivityService : IActivityService
{

    private readonly IActivityStorageEFRepo activityStorageEFRepo;

    public ActivityService (IActivityStorageEFRepo _activityStorageEFRepoFromBuilder)
    {
        activityStorageEFRepo = _activityStorageEFRepoFromBuilder;
    }

    public async Task<Activity> CreateNewActivityAsync(Activity newActivityToAddFromController)
    {
        await activityStorageEFRepo.CreateNewActivityInDBAsync(newActivityToAddFromController);
        return newActivityToAddFromController;
    }

    public async Task<List<Activity>> GetAllActivitiesByUserNameAsync(string username)
    {
        List<Activity> userActivitiesList = await activityStorageEFRepo.GetAllActivitiesbyUserNameFromDBAsync(username);
        return userActivitiesList;
    }

}

