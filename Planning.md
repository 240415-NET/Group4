## Description
--an activity tracker app where you can create a user and keep track of a list of activities (ToDo). 
--Task management - User can login and view tasks, as well as mark them as completed, update(change name), create task, delete task

--User management  
    --Create an account, login with an already account, Delete their account, View User details, Update user information

--Day 2 options 
    --include Admin account for handling deletion of Users (maybe update?) and passwords for all users/admin.
    --link users together (family accounts)

## Models
--User
    --MVP: UserId(Guid), UserName(string), UserEmail(string), User_FirstName(string), User_LastName(string)
    --Day2: Family_Name(string), FamilyMembers(List<string> / List<Person>), 
--Activity
    --MVP: ActivityId(Guid), Activity_Description(string), NameOfPerson(string), Date_OfActivity(DateOnly), Time_OfActivity(TimeOnly)
    --Day2: D




