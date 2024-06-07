using ActivityTracker.Data;


namespace ActivityTracker.Services;
public class ActivityService : IActivityService
{
    private readonly IActivityStorageEFRepo  _activityStorage;

    public ActivityService(IActivityStorageEFRepo eFRepo)
    {
        _activityStorage = eFRepo;
    }

    public async Task<ActivityDTO> AddNewActivityAsync(ActivityDTO activityDTO, string userName)
    {
        await _activityStorage.CreateActivityInDBAsync(activityDTO, userName);
        
        return activityDTO;
    }


public async Task<string>DeleteActivityByActivityNameAsync(string activityDescriptionToDelete, string userName)
{
        await _activityStorage.DeleteActivityByActivityNameFromDBAsync(activityDescriptionToDelete, userName);
        return activityDescriptionToDelete;
}

public async Task<string> DeleteActivityByActivityIdAsync(Guid activityIdToDelete)
{
        return await _activityStorage.DeleteActivityByActivityIdFromDBAsync(activityIdToDelete);
}

}