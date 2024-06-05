using ActivityTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Data;
public interface IUserStorageEFRepo
{
    public Task<User> CreateNewUserInDBAsync(User newUserSentFromUserService);
    public Task<User> GetUserByUserNameFromDBAsync(string userNameToFindFromUserService);
    public Task<string> DeleteUserByUserNameFromDBAsync (string userNameToDeleteFromUserService);

}