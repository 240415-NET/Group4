
using System.ComponentModel.DataAnnotations;

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

}