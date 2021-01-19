## Professional Software Development Project
##### Group CS17 - Tower Building App

# **{App name - Placeholder}**

### Index
 1. Overview - description of the application and its features
 2. Progress Report - Updated 19/11/2020
    - Completed Components - features of the app that have been implemented
    - Work in Progress - features of the app currently being worked on
    - Asset Progress - current state of our assets folders
 3. Plans - Features we intend to include but are not currently working on
 4. Documentation - Explanation of the contents of our GitLab Project Wiki

#
## Overview
**{App name}** is an app made for Android and IOS in Unity. The app is designed to promote studying and socialising at univeristy. The way we do this is by allowing students to customise the buildings of a small university. Serveral buildings {will be} included, each with a unique colour pallette and designs. Users will be able to customise each building to a high degree.

Students {will be} able to earn {xp} by sharing codes and timing their study sessions. This {xp} will contribute to the users global {xp}. This can be compared on a leaderboard with other users to see who has earned the most. Furthermore, each building/school will have their own private {xp}. As users gain more {xp} in a specific subject, further upgardes will be unlocked.

When we say the app is also designed to promote socialising we mean that students will be able to add each other as friends and view eachothers worlds. Each user will have a unique world in that each building will be their own. Something that will make each world especially unique will be the centerpiece of their university, their own custom building.

## Methods of gaining {xp}
Users will be able to gain {xp} through an in-app timer. This timer will run for a maximum of 60minutes. The longer the timer runs, the more {xp} will go towards their global xp and their subject specific {xp}. On the timer screen the user {will be} able to select which subject they plan to study for.

## Customisation
### Subject Specific Buildings
Buildings will each have 2 main materials; walls and windows. Depending on the global xp of the user, a different number of materials will be available for each building. A new material will be unlocked at milestones. E.g. the second wall colour will be unlocked after gaining 1000 {xp}.

Each building corresponds to a subject. As the user levels up a specific subject there will be similar, harder to reach, milestones which unlock entirely new building models.

### Personal User Building
This building will be made up of 4 towers and the user will be able to customise the wall and window colours for each as well as the height of each tower. {we intend to have it so} the materials won't just be colours but {will have} a pattern/texture to make them even more stylish. The colours will unlock at the same rate as the rest of the app and the tower heights will be free to change. This allows the user to establish something they want right at the begining of the game

#
##### Progress Report - Updated 19/11/2020
### Completed components
 - Ability to move, pan and zoom around an enclosed area
 - Ability to customise
   - Change the colour of walls and glass seperatly
   - Change between object models
 - Created a *Demo* user interface
 - Created *Placeholder* assets to test functionality on
 - Created a working backend where data is stored and can be accessed using CRUD requests.

### Work in progress
 - Integrating all the app compoennts in to one Unity project
 - Ability to rotate your view to better look around the map
 - Ability to modify the buildings after reading from JSON
 - Have unity be able to send and recieve data to and from the backend

### Asset progress
 - `Blender Models/`
   - Created placeholder buildings
   - Created terrain concept for the main world
 - `Icons/`
   - Created concept UI features

#
## Plans
There are some features we plan on implementing but that we are not currently working on. 

Something we intend to improve is how users can gain {xp}. Our original plan was just to use the in-app timer. We now intend to add 2 more ways of earning {xp}
 1. A big part of the app is the ability to view other peoples worlds. We want people to be able to see what their friends are up to. Along side this we thought a good feature would be the ability to share {xp}. we intend on having unique codes, maybe distributed everyweek via email. When a user enters this code in their app, they will earn xp. The code will then be deleted from the database. This code will not be applicable to the user who recieved it, meaning they have to distibute it to someone else.
 2. Another feature we would like to implement is a *streak multiplier*. Essentially, for each day a user studies in a row, the {xp} they earn per second/ per minute will be increased. an example of this would be on Day 1 there is no streak, they earn 5{xp} ever 10 seconds. on Day 2 they will earn 6{xp} for every 10 seconds, Day 3, 7{xp} etc. This still needs some refinement but we think this would be a good feature to have.

## Documentation
For information on how we developed this app, check our GitLab Project wiki. It includes:
 - a list of the team of developers, 
 - how to setup a Unity project in GitLab
 - how to port a Unity app to Android

 *If not included yet, the GitLab Project wiki will also explain:*
 - The backend: What utilities and frameworks we used
 - How we developed specific components of the game

