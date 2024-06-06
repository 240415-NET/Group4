using ActivityTracker.Models;
using ActivityTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ActivityTracker.Services;

public class UserService : IUserService
{

    private readonly IUserStorageEFRepo userStorageEFRepo;

    public UserService (IUserStorageEFRepo _userStorageEFRepoFromBuilder)
    {
        userStorageEFRepo = _userStorageEFRepoFromBuilder;
    }
    public async Task<User> CreateNewUserAsync(User userToCreateFromController)
    {
        await userStorageEFRepo.CreateNewUserInDBAsync(userToCreateFromController);
        return userToCreateFromController;
    }

    public async Task<User> GetUserByUserNameAsync(string userNameToFindFromController)
    {
        User foundUser = await userStorageEFRepo.GetUserByUserNameFromDBAsync(userNameToFindFromController);
        return foundUser;
    }

    public async Task<string> DeleteUserByUserNameAsync(string userNameToDeleteFromController)
    {
        await userStorageEFRepo.DeleteUserByUserNameFromDBAsync(userNameToDeleteFromController);
        return userNameToDeleteFromController;
    }

    public async Task<string> UpdateUserNameAsync(string oldUserName, string newUserName)
    {
        return await userStorageEFRepo.UpdateUserInDBAsync(oldUserName, newUserName);

    }


}