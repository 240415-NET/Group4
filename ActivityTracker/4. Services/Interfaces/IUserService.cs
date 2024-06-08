using ActivityTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Services;
public interface IUserService
{
    public Task<User> CreateNewUserAsync(User userToCreateFromController);
    public Task<bool> UserExistsAsync(string userNameToFindFromController);

}