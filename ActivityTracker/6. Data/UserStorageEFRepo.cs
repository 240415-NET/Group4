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

    public async Task<bool> DoesThisUserExistInDBAsync (string userNameToFindFromUserService)
    {
        return await dataContext.users.AnyAsync(user => user.userName == userNameToFindFromUserService);
    }

}