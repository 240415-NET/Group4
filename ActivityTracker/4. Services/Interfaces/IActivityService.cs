using ActivityTracker.Data;

namespace ActivityTracker.Services;

public interface IActivityService
{
    public Task<ActivityDTO> AddNewActivityAsync(ActivityDTO dto, string userName);
    public Task<string>DeleteActivityAsync(string activityDescriptionToDelete, string userName);


}