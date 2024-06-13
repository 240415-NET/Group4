document.addEventListener("DOMContentLoaded", () => {

// Divs
const loginContainer = document.getElementById("login-container");
const userContainer = document.getElementById("logged-in-user-container");
const createUserContainer = document.getElementById("create-new-user-container");

// Buttons
const loginButton = document.getElementById("login-button");
const logoutButton = document.getElementById("logout-button");
const createUserButton = document.getElementById("create-user-button");
const addNewActivity = document.getElementById("add-new-activity");
const deleteActivity = document.getElementById("delete-activity");

// Text & lists
const welcomeMessage = document.getElementById("welcome-message");
const activityList = document.getElementById("activity-list");



// If a user is logged in, get that user from local storage and display the UI for a logged in user
const storedUser = JSON.parse(localStorage.getItem("user"));
if(storedUser)
    {
        updateUIForLoggedInUser(storedUser);
    }

// If the user is not already logged in, when the login button is clicked, try to log them in with the username they provide
loginButton.addEventListener("click", async() =>
{
    if(userNameEnteredByUser.value)
        {
            try
            {
                const response = await fetch(`http://localhost:5289/users?userNameToFindFromFrontEnd=${userNameEnteredByUser.value}`);
                const user = await response.json();
                localStorage.setItem("user", JSON.stringify(user));
                updateUIForLoggedInUser(user);
            } catch(error)
            {
                console.error("Error logging in: ", error);
            }
        }
}); // End of loginButton.addEventListener("click")

// When the user clicks log out, remove the user from localStorage, hide the UI for a logged in user, and display the initial UI
logoutButton.addEventListener("click", () => {

    localStorage.removeItem("user");
    loginContainer.style.display = "block";
    userContainer.style.display = "none";
}); // end of logoutButton.addEventListener("click")

// This function sets up the UI for a logged in user.
// Hide the initial UI (prior to logging in), display a welcome message,
// and display the logged in UI.
// Then go get the user's activities and display 'em.
function updateUIForLoggedInUser(user) {
    loginContainer.style.display="none";
    welcomeMessage.textContent = `Welcome, ${user.userName}!`
    userContainer.style.display= "block";
    fetchUserActivities(user.userName);
}; // end of UpdateUIforLoggedInUser(user) function

// This functin goes and gets the user's activities.
async function fetchUserActivities(userName){
    try{
        const response = await fetch(`http://localhost:5289/GetActivitiesbyUserName ${userName}`);
        const activities = await response.json();
        renderActivityList(activities);
    } catch(error)
    {
        console.error("Error fetching items: ", error);
    }
}; // end of fetchUserActivities(userName) function

function renderActivityList(activities){
    activityList.innerHTML = "";
    activities.forEach(activity => {
        const listItem = document.createElement("li");
        listItem.textContent = `${activity.activity_Description} - ${activity.nameOfPerson} - ${activity.Date_OfActivity} - ${activity.Time_OfActivity} - ${isComplete}`;
        activityList.appendChild(listItem);
    });
};// end of the renderActivityList(activities) function



// CREATE NEW USER
    // Elements related to creating a user
    const showCreateUserButton = document.getElementById("show-create-user-button");
    const userNameEnteredByUser = document.getElementById("existingUserNameFromUser");
    const newUserNameToCreateFromUser = document.getElementById("create-new-username");
    const cancelCreateUserButton = document.getElementById("cancel-create-user-button");
    const newUserEmail = document.getElementById("create-new-email");
    const newUserFirstName = document.getElementById("create-new-first-name");
    const newUserLastName = document.getElementById("create-new-last-name");
    const newUserForm = document.getElementById("create-user-form");

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
        



}); // end of document.addEventListener("DOMContentLoaded")