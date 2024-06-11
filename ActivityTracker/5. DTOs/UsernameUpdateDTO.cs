using Microsoft.EntityFrameworkCore;

using ActivityTracker.Models;
namespace ActivityTracker.Data;



public class UpdateUsernameDTO
{
    public string? oldUserName { get; set; } = string.Empty;
    public string? newUserName { get; set;} = string.Empty;


}
