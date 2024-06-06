using ActivityTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Services;
public interface IUserService
{
    public Task<User> CreateNewUserAsync(User userToCreateFromController);
    public Task<User> GetUserByUserNameAsync(string userNameToFindFromController);
    public Task<string> DeleteUserByUserNameAsync(string userNameToDeleteFromController);

    public Task<string> UpdateUserNameAsync(string oldUserName, string newUserName);
}