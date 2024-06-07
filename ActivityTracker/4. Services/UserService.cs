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

}