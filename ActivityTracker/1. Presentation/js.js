document.addEventListener('DOMContentLoaded', ()=> {

    //div containers
    const loginContainer = document.getElementById('login-container');
    const userContainer = document.getElementById('user-container');
    const createUserContainer = document.getElementById('create-user-container');

    //general elements
    const loginButton = document.getElementById('login-button');
    const logoutButton = document.getElementById('logout-button');
    const welcomeMessage = document.getElementById('welcome-message');
    const usernameInput = document.getElementById('username');
    const activityList = document.getElementById('activity-list');

    //elements for create user
    const showCreateUserButton = document.getElementById('show-create-user-button');
    const newUsernameInput = document.getElementById('new-username');
    const createUserButton = document.getElementById('create-user-button');
    const cancelCreateUserButton = document.getElementById('cancel-create-user-button');

    //elements related to activities
    const createActivityButton = document.getElementById('create-activity-button');
    const updateActivityButton = document.getElementById('update-activity-button');
    const deleteActivityButton = document.getElementById('delete-activity-button');


    //if a user is already logged in...
    const storedUser = JSON.parse(localStorage.getItem('user'));
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

}) //EndDOMContentLoaded listener