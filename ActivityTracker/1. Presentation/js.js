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

    //elements related to adding a new activity



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

            } catch (error) {
                console.error('Error logging in: ', error);
            }

        } //End If to check username for content
    }) //End of the login button event listener

    function updateUIForLoggedInUser(user) {

        loginContainer.style.display = 'none';
        welcomeMessage.textContent = `Welcome ${user.userName}!`;
        userContainer.style.display = 'block';

        //fetchUserActivities(user)

    } //end updateUIforLoggedInUser

    logoutButton.addEventListener('click', () => {

        localStorage.removeItem('user');
        usernameInput.value = null;          

        loginContainer.style.display = 'block';
        userContainer.style.display = 'none';

    }); //end of the logoutButton event listener
















}) //EndDOMContentLoaded listener