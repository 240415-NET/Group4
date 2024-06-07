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

}