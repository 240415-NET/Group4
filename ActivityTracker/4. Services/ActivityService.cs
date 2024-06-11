using ActivityTracker.Data;
using ActivityTracker.Models;


namespace ActivityTracker.Services;
public class ActivityService : IActivityService
{
    private readonly IActivityStorageEFRepo _activityStorage;

    public ActivityService(IActivityStorageEFRepo eFRepo)
    {
        _activityStorage = eFRepo;
    }

    public async Task<ActivityDTO> AddNewActivityAsync(ActivityDTO activityDTO, string userName)
    {
        ActivityDTO activity = await _activityStorage.CreateActivityInDBAsync(activityDTO, userName);

        return activity;
    }


    public async Task<string> DeleteActivityByActivityNameAsync(string activityDescriptionToDelete, string userName)
    {
        await _activityStorage.DeleteActivityByActivityNameFromDBAsync(activityDescriptionToDelete, userName);
        return activityDescriptionToDelete;
    }

    public async Task<string> DeleteActivityByActivityIdAsync(Guid activityIdToDelete)
    {
        return await _activityStorage.DeleteActivityByActivityIdFromDBAsync(activityIdToDelete);
    }

    public async Task<string> UpdateActivityByActivityIdAsync(Guid activityIdToUpdateFromController)
    {
        try
        {
            return await _activityStorage.UpdateActivityByActivityIdFromDBAsync(activityIdToUpdateFromController);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);  
        }
    }

    public async Task<List<ActivityDTO>> GetAllActivitiesbyUserNameAsync(string userNameFromController)
    {
        List<ActivityDTO> foundItems = new();

        //We know we will get something back from the data access layer
        //I've got some assumptions about what it is, but lets say I'm a little lazy
        //We can leverage "var" to make things easier for us 

        var resultList = await _activityStorage.GetAllActivitiesForUserFromDBAsync(userNameFromController);

        foreach (var activity in resultList)
        {
            //For each item model object in our result list, we will call that mapping constructor 
            //that takes an item and uses it to create an ItemDTO for us. Then it adds that new ItemDTO object
            //to the foundItems list we created above.
            foundItems.Add(new ActivityDTO(activity));
        }

        return foundItems;
    }

}