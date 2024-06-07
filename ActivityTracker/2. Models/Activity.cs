
using System.ComponentModel.DataAnnotations;
using ActivityTracker.Data;

namespace ActivityTracker.Models;
public class Activity
{
    [Key]
    public Guid activityID {get; set;}
    public string activity_Description {get;set;}
    public string nameOfPerson {get;set;}
    public DateOnly Date_OfActivity {get;set;}
    public TimeOnly Time_OfActivity {get;set;}
    public User user {get; set;} = new();

    public Activity(){}


    public Activity(ActivityDTO activityDTO, User user)
    {
        activityID = Guid.NewGuid();
        activity_Description = activityDTO.activity_Description;
        nameOfPerson = activityDTO.nameOfPerson;
        Date_OfActivity = DateOnly.Parse(activityDTO.Date_OfActivity);
        Time_OfActivity = TimeOnly.Parse(activityDTO.Time_OfActivity);
        this.user = user;
    }

}