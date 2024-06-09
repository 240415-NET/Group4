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
        if(await UserExistsAsync(userToCreateFromController.userName))
        {
            throw new Exception("This user already exists in the database.");
        }
        if(String.IsNullOrEmpty(userToCreateFromController.userName))
        {
            throw new Exception("User name can't be blank.");
        }
        await userStorageEFRepo.CreateNewUserInDBAsync(userToCreateFromController);
        return userToCreateFromController;
    }

    public async Task<bool> UserExistsAsync(string userNameToFindFromController)
    {
        return await userStorageEFRepo.DoesThisUserExistInDBAsync(userNameToFindFromController);
    }

    public async Task<User> GetUserByUserNameAsync(string userNameToFindFromController)
    {
        try
        {
            User foundUser = await userStorageEFRepo.GetUserByUserNameFromDBAsync(userNameToFindFromController);
            return foundUser;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}