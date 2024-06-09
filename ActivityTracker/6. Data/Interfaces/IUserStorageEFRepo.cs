using ActivityTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Data;
public interface IUserStorageEFRepo
{
    public Task<User> CreateNewUserInDBAsync(User newUserSentFromUserService);
    public Task<bool> DoesThisUserExistInDBAsync (string userNameToFindFromUserService);
    public Task<User> GetUserByUserNameFromDBAsync(string userNameToFindFromUserService);
}