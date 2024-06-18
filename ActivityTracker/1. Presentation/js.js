
document.addEventListener("DOMContentLoaded", () => {

    // Divs/Forms
    const loginContainer = document.getElementById("login-container");
    const loginContainerHeader = document.getElementById("login-container-header");
    const userContainer = document.getElementById("logged-in-user-container");
    const createUserContainer = document.getElementById("create-new-user-container");
    const userInfoContainer = document.getElementById("user-info-container");
    const userInfoButtonContainer = document.getElementById("user-info-button-container");
    const activityForm = document.getElementById('add-activity-form');
    const updateUserNameContainer = document.getElementById('update-username-container');
    const updateUserNameForm = document.getElementById('update-username-form');

    // Buttons
    const loginButton = document.getElementById("login-button");
    const logoutButton = document.getElementById("logout-button");
    const createUserButton = document.getElementById("create-user-button");
    //const addNewActivity = document.getElementById("add-new-activity");
    //const deleteActivity = document.getElementById("delete-activity-button");
    const deleteUserButton = document.getElementById('delete-user-button');
    const cancelAddActivityButton = document.getElementById('cancel-add-activity-button');
    const userInfoButton = document.getElementById('user-info-button');
    const updateUserNameButton = document.getElementById('update-username-button')
    const ReturnHomeButton = document.getElementById('return-home-button');
    const saveUserInfoButton = document.getElementById('save-userInfo-button');
    const cancelUpdateUserButton = document.getElementById('cancel-updateUser-button');
    
    
    

    // Text & lists
    const welcomeMessage = document.getElementById("welcome-message");
    const activityList = document.getElementById("activity-list");
    const userInfoList = document.getElementById("userinfo-list")

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
    const updateOldUserName = document.getElementById("update-old-username");
    const updateNewUserName = document.getElementById("update-new-username");


    //elements related to activities
    const showCreateActivityButton = document.getElementById('create-activity-button');
    const showCreateActivityForm = document.getElementById('add-activity-container');
    const updateActivityButton = document.getElementById('update-activity-button');
    const deleteActivityButton = document.getElementById('delete-activity-button');

    


    // If a user is logged in, get that user from local storage and display the UI for a logged in user
    const storedUser = JSON.parse(localStorage.getItem("user"));
    if (storedUser) {
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
            catch (error) {
                console.error('Error logging in: ', error);
                alert(`${username} does not exist. Please try again.`);
            }

        } //End If to check username for content
    }) //End of the login button event listener

    function updateUIForLoggedInUser(user) {

        loginContainer.style.display = 'none';
        loginContainerHeader.style.display= 'none';
        welcomeMessage.textContent = `Welcome, ${user.userName}!`;
        userContainer.style.display = 'block';
        showCreateActivityForm.style.display = 'none';
        userInfoButtonContainer.style.display="block";
        userInfoButtonContainer.style.display="block";
        updateUserNameContainer.style.display = "none";
        

        fetchUserActivities(user.userName);

    } //end updateUIforLoggedInUser

    logoutButton.addEventListener('click', () => {

        localStorage.removeItem('user');

        usernameInput.value = null;
        loginContainer.style.display = 'block';
        loginContainerHeader.style.display = 'block';
        userContainer.style.display = 'none';
        showCreateActivityForm.style.display = 'none';
        userInfoButtonContainer.style.display = "none";
        updateUserNameContainer.style.display = "none";
        userInfoContainer.style.display="none";

    }); //end of the logoutButton event listener


    // CREATE NEW USER

    // MOVE FROM LOG-IN SCREEN TO CREATE USER SCREEN WHEN YOU CLICK CREATE USER
    showCreateUserButton.addEventListener('click', () => {
        loginContainer.style.display = 'none';
        loginContainerHeader.style.display = 'none';
        createUserContainer.style.display = 'block';
    });//End create user button listener

    // MOVE FROM CREATE USER SCREEN BACK TO LOG-IN WHEN YOU CLICK CANCEL
    cancelCreateUserButton.addEventListener('click', () => {
        loginContainer.style.display = 'block';
        loginContainerHeader.style.display = 'block';
        createUserContainer.style.display = 'none';
    });//End cancel create user button listener


    // IN THE CREATE USER SCREEN, CREATE A NEW USER WHEN THE CREATE USER BUTTON IS CLICKED
    newUserForm.addEventListener("submit", async (event) => {

        event.preventDefault();


        if (newUserNameToCreateFromUser.value) {

            const newUser =
            {
                userId: "00000000-0000-0000-0000-000000000000",
                userName: newUserNameToCreateFromUser.value,
                userEmail: newUserEmail.value,
                user_FirstName: newUserFirstName.value,
                user_LastName: newUserLastName.value,
                activityList: []
            }

            try {
                const response = await fetch(`http://localhost:5289/users?userNameFromFrontEnd=${newUserNameToCreateFromUser.value}&userEmail=${newUserEmail.value}&firstName=${newUserFirstName.value}&lastName=${newUserLastName.value}`, {
                    method: "POST",
                    body: JSON.stringify(newUser)
                });
                const newlyCreatedUser = await response.json();
                localStorage.setItem("user", JSON.stringify(newlyCreatedUser));
                updateUIForLoggedInUser(newlyCreatedUser);
                createUserContainer.style.display = "none";
                userContainer.style.display = "block";
            }
            catch (error) {
                console.error("Error creating user: ", error);
                alert(`${newUserNameToCreateFromUser.value} already exists. Please select another username.`);
                newUserNameToCreateFromUser.value ="";
            }
        }
        else {
            alert("Username cannot be blank");
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
        
        const sortedActivities = activities.sort(
            (a, b) => new Date(a.date_OfActivity + " " + a.time_OfActivity) 
                    - new Date(b.date_OfActivity + " " + b.time_OfActivity));
 
        sortedActivities.forEach(activity => {
            const listItem = document.createElement('option');
            const isActivityCompleted = activity.isComplete;
     
            listItem.text = `${activity.activity_Description}, ${activity.nameOfPerson}, ${activity.date_OfActivity}, ${activity.time_OfActivity}`;
            listItem.value = activity.activityId;
            listItem.style.color = isActivityCompleted ? '#969696' : '#003399';  // #969696 is grey, #003399 is dark blue
            listItem.style.textDecoration = isActivityCompleted ? 'line-through' : '';
            activityList.add(listItem);
        });

    } // End renderActivitiesList


    updateActivityButton.addEventListener('click', async () => {

        const activityIdToUpdate = activityList.value;

        if (activityIdToUpdate) // if an activity if selected in the activity list
        {
            try 
            {
                const response = await fetch(`http://localhost:5289/UpdateActivityByActivityId?activityIdToUpdateFromFrontEnd=${activityIdToUpdate}`, {
                        method: 'PATCH',
                        headers: {
                            'Content-Type' : 'text/plain; charset=utf-8'
                        }
                    },
                );
            }          
            catch (error) {
                console.error('Error updating activity: ', error);
            }
        }
        refreshUserActivityList();

    }); //End of updateActivityButton event listener

    showCreateActivityButton.addEventListener('click', () => {
        showCreateActivityForm.style.display = 'block';
    });//End create activity button listener


    activityForm.addEventListener('submit', async (event) => {
        event.preventDefault();

        const descriptionFromForm = document.getElementById('description');
        const nameOfPersonFromForm = document.getElementById('nameOfPerson');
        const dateFromForm = document.getElementById('date');
        const timeFromForm = document.getElementById('time');


        const newActivity = {
            activityId: "00000000-0000-0000-0000-000000000000",
            activity_Description: descriptionFromForm.value,
            nameOfPerson: nameOfPersonFromForm.value,
            date_OfActivity: dateFromForm.value,
            time_OfActivity: timeFromForm.value
        };

        const loggedInUser = JSON.parse(localStorage.getItem('user'));

        try {
            const response = await fetch(`http://localhost:5289/Activity/${loggedInUser.userName}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newActivity)
            });
            fetchUserActivities(loggedInUser.userName);
            showCreateActivityForm.style.display = 'block';
            activityForm.reset();

        }
        catch (error) {
            console.error("Error:", error);
        }
    }); // End of activityForm listner

    cancelAddActivityButton.addEventListener('click', async (event) => {
        event.preventDefault();
        const loggedInUser = JSON.parse(localStorage.getItem('user'));
        updateUIForLoggedInUser(loggedInUser);

    }); //End of cancelAddActivityButton listner
  
    deleteUserButton.addEventListener('click', async () => {
        const loggedInUser = JSON.parse(localStorage.getItem('user'));

        const response = await fetch
            (`http://localhost:5289/users?userName=${loggedInUser.userName}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            localStorage.removeItem('user');
            document.getElementById('username').value = "";
            
    }); // END of deleteUserButton Listner

    function refreshUserActivityList() {
        //get the current user from local storage, then get the user's activities
        const loggedInUser = JSON.parse(localStorage.getItem('user'));
        fetchUserActivities(loggedInUser.userName);

    } //end refreshUserActivities


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

    // MOVE FROM ACTIVITY SCREEN TO VIEW USER PROFILE WHEN YOU CLICK USER INFO BUTTON
    userInfoButton.addEventListener('click', () => {
        userContainer.style.display = 'none';
        userInfoContainer.style.display = 'block';
        userInfoButtonContainer.style.display = 'block';
        showCreateActivityForm.style.display = "none";
        
        
    });//End UserInfo button listener

    // MOVE FROM USRE INFO SCREEN BACK TO ACTIVITIES WHEN YOU CLICK CANCEL
    ReturnHomeButton.addEventListener('click', () => {
        userContainer.style.display = 'block';
        userInfoContainer.style.display = 'none';
        userInfoButtonContainer.style.display = 'block';
        updateUserNameContainer.style.display = "none";
    });//End ReturnHome  button listener

     //Listening for a click on the userInfo button
     userInfoButton.addEventListener('click', async () => {

        const loggedInUser = JSON.parse(localStorage.getItem('user'));
            fetchUserInfo(loggedInUser.userName)

       
    }) //End of the login button event listener

    async function fetchUserInfo(username) {
        try {

            const response = await fetch(`http://localhost:5289/users?userNameToFindFromFrontEnd=${username}`);
            const userInfo= await response.json();
            localStorage.setItem("user", JSON.stringify(userInfo));

            renderUserInfoList(userInfo);

        } catch (error) {
            console.error('Error fetching UserInfo: ', error);
        }

    } // End fetchUserInfo


    function renderUserInfoList(userInfo) {

       // console.log (userInfo);
        userInfoList.innerHTML = "";
       
        //const listItem = document.getElementById("userinfo-list")
        //     const listItem = document.createElement('option');
  
        const listItem = `  <strong>Username:</strong>   ${userInfo.userName} <br>
                            <strong>Email</strong>:      ${userInfo.userEmail} <br> 
                            <strong> First Name</strong>:   ${userInfo.user_FirstName} <br>
                            <strong> Last Name</strong>:    ${userInfo.user_LastName}`;
       userInfoList.innerHTML = listItem;
        

    } // End renderUserInfoList
    
    ///CODE TO UPDATE USER NAME OF USER:
    
    updateUserNameButton.addEventListener('click', () => {
        updateUserNameContainer.style.display = 'block';
        userInfoContainer.style.display = 'block';
        userInfoButtonContainer.style.display='block';
        updateUserNameForm.reset();

        
    });//End UserInfobutton listener

    // MOVE FROM USRE INFO SCREEN BACK TO ACTIVITIES WHEN YOU CLICK CANCEL
    ReturnHomeButton.addEventListener('click', () => {
        userContainer.style.display = 'block';
        userInfoContainer.style.display = 'none';
        userInfoButtonContainer.style.display='block';
        const storedUser = JSON.parse(localStorage.getItem("user"));
        updateUIForLoggedInUser(storedUser);
    });//End ReturnHome button listener

    cancelUpdateUserButton.addEventListener('click', () => {
        updateUserNameContainer.style.display = 'none';
        userInfoContainer.style.display = 'block';
        userInfoButtonContainer.style.display='block';
    });//End CancelUserUpdate  button listener
    deleteUserButton.addEventListener('click', () => {
        
        loginContainer.style.display = 'block';
        loginContainerHeader.style.display = 'block';
        userInfoContainer.style.display = 'none';
        userInfoButtonContainer.style.display = 'none';

    });//End DeleteUserbutton listener
    
    saveUserInfoButton.addEventListener("click", async (event) => {

        event.preventDefault();
        const loggedInUser = JSON.parse(localStorage.getItem('user'));
        const OldUserName= loggedInUser.userName;
        const updatedNewUserName=updateNewUserName.value;

        if (updateNewUserName.value) {

            const newUserNameObject =
            {
                
                oldUserName: OldUserName,
                newUserName: updatedNewUserName
               
            }

            try {
                const response = await fetch(`http://localhost:5289/users`, 
                {
                    method: 'PATCH',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(newUserNameObject)
            });
               //const newlyCreatedUser = await response.json();
               

               
               const user = JSON.parse(localStorage.getItem("user"));
               user.newUserName = updateNewUserName.value;
               localStorage.setItem("user", JSON.stringify(user));
               fetchUserInfo(user.newUserName)
               updateUserNameContainer.style.display = 'none';
                userInfoContainer.style.display = "block";
                userInfoButtonContainer.style.display = "block";
              
            }
            catch (error) {
                console.error("Error updating username: ", error);
               // alert(`${updateNewUserName.value} already exists. Please select another username.`);

            }
        }
        else {
            alert("Username cannot be blank");
        }
    }); // End of updateusernameForm.addEventListener 
}) //EndDOMContentLoaded listener
