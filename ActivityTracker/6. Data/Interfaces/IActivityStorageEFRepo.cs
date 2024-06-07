
namespace ActivityTracker.Data;

public interface IActivityStorageEFRepo
{
    public Task<ActivityDTO> CreateActivityInDBAsync(ActivityDTO dto, string userName);
}