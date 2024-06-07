using ActivityTracker.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
namespace ActivityTracker.Data;
public class ActivityStorageEFRepo : IActivityStorageEFRepo
{
    private readonly DataContext dataContext;
    public ActivityStorageEFRepo(DataContext dataContextFromBuilder)
    {
        dataContext = dataContextFromBuilder;
    }

    public async Task<Activity> CreateNewActivityInDBAsync (Activity newActivityToAddFromActivityService)
    {
        User? fullUserWhoseActivityIsBeingAdded = await dataContext.users.SingleOrDefaultAsync(user => user.userName == newActivityToAddFromActivityService.user.userName);
        newActivityToAddFromActivityService.user = fullUserWhoseActivityIsBeingAdded;
  
        dataContext.Add(newActivityToAddFromActivityService);

        await dataContext.SaveChangesAsync();
        return newActivityToAddFromActivityService;
    }

    public async Task<List<Activity>> GetAllActivitiesbyUserNameFromDBAsync(string username)
    {
        List<Activity> userActivitiesListFromDB = new List<Activity>();
        User? fullUserWhoseActivityIsBeingRetrieved = await dataContext.users.SingleOrDefaultAsync(user => user.userName == username);
        Guid userIdToSearchFor = fullUserWhoseActivityIsBeingRetrieved.userId;
        userActivitiesListFromDB = await dataContext.activities // Ask the context for the collection of activities in the database
            .Where(activity => activity.user.userId == userIdToSearchFor) // Get every activity who's userId matches the one we want
            .ToListAsync(); // Turn those items into a list

        return userActivitiesListFromDB;
    }

}