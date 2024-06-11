using System.ComponentModel.DataAnnotations;
using ActivityTracker.Data;


namespace ActivityTracker.Models;
public class User
{
    [Key]
    public Guid userId {get; set;}
    public string userName {get; set;}
    public string userEmail {get; set;}
    public string user_FirstName {get; set;}
    public string user_LastName {get; set;}

    public List<Activity> activityList {get; set;} = new();

    public User(){}
    public User(string _userName, string _userEmail, string _user_FirstName, string _user_LastName)
    {
        userId = Guid.NewGuid();
        userName = _userName;
        userEmail = _userEmail;
        user_FirstName = _user_FirstName;
        user_LastName = _user_LastName;
    }
   
 
}