using ActivityTracker.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ActivityTracker.Data;
public class UserStorageEFRepo : IUserStorageEFRepo
{
    private readonly DataContext _dataContext;
    public UserStorageEFRepo(DataContext dataContextFromBuilder)
    {
        _dataContext = dataContextFromBuilder;
    }
    public async Task<User> CreateNewUserInDBAsync(User newUserSentFromUserService)
    {
        _dataContext.users.Add(newUserSentFromUserService);
        await _dataContext.SaveChangesAsync();
        return newUserSentFromUserService;
    }

    public async Task<bool> DoesThisUserExistInDBAsync(string userNameToFindFromUserService)
    {
            return await _dataContext.users.AnyAsync(user => user.userName == userNameToFindFromUserService);
    }

    public async Task<string> DeleteUserinDBAsync(string userName)
    {
        User? userToDelete = await _dataContext.users.FirstOrDefaultAsync(
            user => user.userName == userName);

        if (userToDelete != null)
        {
            _dataContext.users.Remove(userToDelete);
            await _dataContext.SaveChangesAsync();
            return $"{userToDelete.userName} was removed";
        }
        else
        {
            throw new Exception("User not found");

        }

    }

}