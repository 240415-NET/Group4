using ActivityTracker.Models;
using ActivityTracker.Data;

namespace ActivityTracker.Services;

public interface IActivityService
{
    public Task<ActivityDTO> AddNewActivityAsync(ActivityDTO dto, string userName);
    public Task<string>DeleteActivityByActivityNameAsync(string activityDescriptionToDelete, string userName);
    public Task<string> DeleteActivityByActivityIdAsync(Guid activityIdToDelete);
    public Task<string> UpdateActivityByActivityIdAsync(Guid activityIdToUpdateFromController);
    public  Task<List<ActivityDTO>> GetAllActivitiesbyUserNameAsync(string userName);

}