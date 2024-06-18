using Microsoft.EntityFrameworkCore;
using ActivityTracker.Models;

namespace ActivityTracker.Data;


public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> users { get; set; }
    public DbSet<Activity> activities { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {   

    //     //In this line, we tell our SQL database to become case sensitive. MS SQL Server, for some reason or another,
    //     //is case insensitive for strings, by default. By including this in our OnModelCreating override, we have EF
    //     //toggle this setting so that our strings in our DB obey case sensitivity. 
    //     modelBuilder.UseCollation("SQL_Latin1_General_CP1_CS_AS");

    // }
}