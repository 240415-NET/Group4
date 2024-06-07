using System.Reflection.Metadata.Ecma335;
using ActivityTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Linq;

namespace ActivityTracker.Data;

public class ActivityStorageEFRepo : IActivityStorageEFRepo
{
    private readonly DataContext _context;
    public ActivityStorageEFRepo(DataContext contextFromBuilder)
    {
        _context = contextFromBuilder;
    }

    public async Task<ActivityDTO> CreateActivityInDBAsync(ActivityDTO activityDTO, string userName)
    {
        User owner = await _context.users.SingleOrDefaultAsync(
            user => user.userName == userName);
        Activity newActivity = new(activityDTO, owner);

        _context.activities.Add(newActivity);
        await _context.SaveChangesAsync();
        return activityDTO;
    }

    public async Task<string> DeleteActivityFromDBAsync(string activityDescriptionToDelete, string userName)
    {
        User userToDeleteFrom = await _context.users.SingleOrDefaultAsync(user=>user.userName == userName);
        Guid userIdToDeleteFrom = userToDeleteFrom.userId;

        List<Activity> userActivitiesFromDB = new List<Activity>();

        userActivitiesFromDB = await _context.activities // Ask the context for the collection of activities in the database
            .Where(activity => activity.user.userId == userIdToDeleteFrom) // Get every activity who's userId matches the one we want
            .ToListAsync(); // Turn those items into a list

        Activity activityToDelete = userActivitiesFromDB.SingleOrDefault(activity=>activity.activity_Description == activityDescriptionToDelete);
        
        _context.activities.Remove(activityToDelete);
        await _context.SaveChangesAsync();
        return activityDescriptionToDelete;

    }
}