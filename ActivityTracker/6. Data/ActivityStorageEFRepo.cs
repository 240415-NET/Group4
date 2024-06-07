using ActivityTracker.Models;
using Microsoft.EntityFrameworkCore;

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
}