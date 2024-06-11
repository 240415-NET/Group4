using ActivityTracker.Data;
using ActivityTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Services;
public interface IUserService
{
    public Task<User> CreateNewUserAsync(User userToCreateFromController);
    public Task<bool> UserExistsAsync(string userNameToFindFromController);
    public Task<User> GetUserByUserNameAsync(string userNameToFindFromController);

    public Task<string> DeleteUserAsync(string userName);
    public Task<string> UpdateUsernameAsync(UpdateUsernameDTO userNameToChangeFromController);
}