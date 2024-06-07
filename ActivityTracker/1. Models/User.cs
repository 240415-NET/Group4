using System.ComponentModel.DataAnnotations;


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
    public User(string _userName)
    {
        userId = Guid.NewGuid();
        userName = _userName;
        userEmail = "";
        user_FirstName = "";
        user_LastName = "";
    }
 
}