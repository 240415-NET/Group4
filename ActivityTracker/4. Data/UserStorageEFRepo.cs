using ActivityTracker.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ActivityTracker.Data;
public class UserStorageEFRepo : IUserStorageEFRepo
{
    private readonly DataContext dataContext;
    public UserStorageEFRepo(DataContext dataContextFromBuilder)
    {
        dataContext = dataContextFromBuilder;
    }
    public async Task<User> CreateNewUserInDBAsync(User newUserSentFromUserService)
    {
        dataContext.users.Add(newUserSentFromUserService);
        await dataContext.SaveChangesAsync();
        return newUserSentFromUserService;
    }

    public async Task<User> GetUserByUserNameFromDBAsync(string userNameToFindFromUserService)
    {
        User foundUser = await dataContext.users.SingleOrDefaultAsync(user => user.userName == userNameToFindFromUserService);
        return foundUser;
    }

    public async Task<string> DeleteUserByUserNameFromDBAsync (string userNameToDeleteFromUserService)
    {
        User userToDelete = await GetUserByUserNameFromDBAsync(userNameToDeleteFromUserService);
        dataContext.users.Remove(userToDelete);
        await dataContext.SaveChangesAsync();
        return userNameToDeleteFromUserService;

    }
    
    public async Task<string> UpdateUserInDBAsync(string oldUserName, string newUserName)
    {
        User? userToUpdate = await GetUserByUserNameFromDBAsync(oldUserName);
        userToUpdate.userName = newUserName;
        dataContext.SaveChangesAsync();
        return newUserName;
    }


}