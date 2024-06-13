
document.addEventListener("DOMContentLoaded", () => {

// Divs
const loginContainer = document.getElementById("login-container");
const userContainer = document.getElementById("logged-in-user-container");
const createUserContainer = document.getElementById("create-new-user-container");

// Buttons
const loginButton = document.getElementById("login-button");
const logoutButton = document.getElementById("logout-button");
const createUserButton = document.getElementById("create-user-button");
//const addNewActivity = document.getElementById("add-new-activity");
//const deleteActivity = document.getElementById("delete-activity-button");

// Text & lists
const welcomeMessage = document.getElementById("welcome-message");
const activityList = document.getElementById("activity-list");
  
// Log in existing user
const usernameInput = document.getElementById('username');

 
    // Elements related to creating a user
  const showCreateUserButton = document.getElementById("show-create-user-button");
  const newUserNameToCreateFromUser = document.getElementById("create-new-username");
  const cancelCreateUserButton = document.getElementById("cancel-create-user-button");
  const newUserEmail = document.getElementById("create-new-email");
  const newUserFirstName = document.getElementById("create-new-first-name");
  const newUserLastName = document.getElementById("create-new-last-name");
  const newUserForm = document.getElementById("create-user-form");


  //elements related to activities
 const createActivityButton = document.getElementById('create-activity-button');
 const updateActivityButton = document.getElementById('update-activity-button');
 const deleteActivityButton = document.getElementById('delete-activity-button');



// If a user is logged in, get that user from local storage and display the UI for a logged in user
const storedUser = JSON.parse(localStorage.getItem("user"));
if(storedUser)
    {
        updateUIForLoggedInUser(storedUser);
    }
  

  
    //listening for a click on the login button
    loginButton.addEventListener('click', async () => {

        const username = usernameInput.value;

        if (username) {

            try {    
                const response = await fetch(`http://localhost:5289/users?userNameToFindFromFrontEnd=${username}`);
                const user = await response.json();

                localStorage.setItem('user', JSON.stringify(user));
                updateUIForLoggedInUser(user);                 
            }
            catch (error) 
            {
                console.error('Error logging in: ', error);
                alert(`Unable to login with username: ${username}`);                             
            }

        } //End If to check username for content
    }) //End of the login button event listener

    function updateUIForLoggedInUser(user) {

        loginContainer.style.display = 'none';
        welcomeMessage.textContent = `Welcome ${user.userName}!`;
        userContainer.style.display = 'block';

        fetchUserActivities(user.userName);

    } //end updateUIforLoggedInUser

    logoutButton.addEventListener('click', () => {

        localStorage.removeItem('user');

        usernameInput.value = null;          
        loginContainer.style.display = 'block';
        userContainer.style.display = 'none';

    }); //end of the logoutButton event listener

  
// CREATE NEW USER

 // MOVE FROM LOG-IN SCREEN TO CREATE USER SCREEN WHEN YOU CLICK CREATE USER
    showCreateUserButton.addEventListener('click', () => {
        loginContainer.style.display = 'none';
        createUserContainer.style.display = 'block';
    });//End create user button listener

 // MOVE FROM CREATE USER SCREEN BACK TO LOG-IN WHEN YOU CLICK CANCEL
    cancelCreateUserButton.addEventListener('click', () => {
        loginContainer.style.display = 'block';
        createUserContainer.style.display = 'none';
    });//End cancel create user button listener


    // IN THE CREATE USER SCREEN, CREATE A NEW USER WHEN THE CREATE USER BUTTON IS CLICKED
    newUserForm.addEventListener("submit", async(event)=> {

        event.preventDefault();
    

        if(newUserNameToCreateFromUser.value) {

            const newUser = 
            {
                userId: "00000000-0000-0000-0000-000000000000",
                userName: newUserNameToCreateFromUser.value,
                userEmail: newUserEmail.value,
                user_FirstName: newUserFirstName.value, 
                user_LastName: newUserLastName.value,
                activityList: []
            }

            try{               
                const response = await fetch (`http://localhost:5289/users?userNameFromFrontEnd=${newUserNameToCreateFromUser.value}&userEmail=${newUserEmail.value}&firstName=${newUserFirstName.value}&lastName=${newUserLastName.value}`, {
                method: "POST",
                body: JSON.stringify(newUser)
                });
            const newlyCreatedUser = await response.json();
            localStorage.setItem("user", JSON.stringify(newlyCreatedUser));
            updateUIForLoggedInUser(newlyCreatedUser);
            createUserContainer.style.display="none";
            userContainer.style.display = "block";
        }
        catch(error)
        {
            console.error ("Error creating user: ", error);
            alert();
        }
    }
        else {
            alert ("Username cannot be blank");
        }
}); // End of createUserButton.addEventListener 
        


    async function fetchUserActivities(username) {
        try {
        
            const response = await fetch(`http://localhost:5289/GetActivitiesbyUserName/${username}`);
            const activities = await response.json();

            renderActivityList(activities);

        } catch (error) {
            console.error('Error fetching activities: ', error);
        }

    } // End fetchUserActivities


    function renderActivityList(activities) {

        activityList.innerHTML = '';

        let listItemValue = 0;
        activities.forEach( activity => {
            listItemValue++;
            const listItem = document.createElement('option');
            listItem.text = `${activity.activity_Description}, ${activity.nameOfPerson}, ${activity.date_OfActivity}, ${activity.time_OfActivity}`;
            listItem.value = activity.activityId;
            activityList.add(listItem);
        });

    } // End renderActivitiesList


    updateActivityButton.addEventListener('click', async () => {

        const activityIdToUpdate = activityList.value;

        if (activityIdToUpdate) // if an activity if selected in the activity list
        {
            try 
            {
                const response = await fetch(`http://localhost:5289/UpdateActivityByActivityId?activityIdToUpdateFromFrontEnd=${activityIdToUpdate}`);  
            }
            catch (error)
            {
                console.error('Error updating activity: ', error);
            }
        }

    }); //End of updateActivityButton event listener



















// DELETE ACTIVITY
    deleteActivityButton.addEventListener("click", async () =>{

        const activityIdToDelete = activityList.value;
        if (activityIdToDelete)
            {
            try
            {
                const response = await fetch(`http://localhost:5289/DeleteActivityByActivityId?activityIdToDelete=${activityIdToDelete}`, {
                    method: "DELETE"
                });
                const storedUser = JSON.parse(localStorage.getItem("user"));
                fetchUserActivities(storedUser.userName);
            }
            catch(error)
            {
                console.error("Error deleting activity: ", error);
            }
        }
    }); // End of deleteActivityButton.addEventListener("click",...

}) //EndDOMContentLoaded listener

