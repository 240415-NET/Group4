using ActivityTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Data;
public interface IUserStorageEFRepo
{
    public Task<User> CreateNewUserInDBAsync(User newUserSentFromUserService);
}