
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration.UserSecrets;

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

    public Activity (string _activity_Description, string _nameOfPerson,
                DateOnly _Date_OfActivity, TimeOnly _Time_OfActivity)
        {
        activityID = Guid.NewGuid();
        activity_Description = _activity_Description;
        nameOfPerson = _nameOfPerson;
        Date_OfActivity = _Date_OfActivity;
        Time_OfActivity = _Time_OfActivity;
    }

}