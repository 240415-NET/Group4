
namespace ActivityTracker.Data;

public interface IActivityStorageEFRepo
{
    public Task<ActivityDTO> CreateActivityInDBAsync(ActivityDTO dto, string userName);

    public Task<string> DeleteActivityFromDBAsync(string activityDescriptionToDelete, string userName);

}