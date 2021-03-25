## Professional Software Development Project
##### Group CS17 - Tower Building App

# **UNI BUILDER**

[[_TOC_]]

# Overview
**Uni Builder** is an app made for Android and IOS in Unity. The app is designed to promote studying and socialising at university. We do this by allowing students to customise the department buildings of a small university. Several buildings are included, each with the option for the user to create a unique colour pallette and design. Users are able to customise each building with a primary and secondary material from a range of over 40 colours and textures.

Students are able to earn xp by <!--sharing codes with friends and/or--> timing their study sessions. The xp gathered will contribute to the users global xp count. This can be compared on a leader board with other users. Furthermore, each department building will have their own personal xp count. As users study for specific subjects, they will gain more xp for that specific subject, unlocking a variety of building models.

The app is also designed to promote socialising. Students are able to follow each other as friends and view each others worlds. Something that will make each world especially unique will be the centerpiece of their university, a much more customisable building.

# Methods of gaining xp
Users will be able to gain xp through an in-app timer. <!--This timer will run for a maximum of 60 minutes.--> The longer the timer runs, the more xp will go towards their global xp and their subject specific xp. On the timer screen the user is able to select which subject they plan to study for in order to direct the subject specific xp accordingly.

As you earn xp, 10% of what you earn will be added to every subejct that is not the focus of the study session. For example: If you study for 100 seconds for mathematics, you will earn 100xp for maths. 100xp will also be added to your global xp count, *and* 10xp will also be added to every other subject. This allows for continious, cross-subject progression.

# Customisation
## Subject Specific Buildings
### Materials
Buildings will each have 2 main materials; Primary and Secondary. Depending on the global xp of the user, a different number of materials will be available for their buildings. A new material will be unlocked at milestones. 

Each milestone/level is {15000xp}.  
 - Every level - 15,000xp you will unlock a new matte colour. A colour with not much reflectivity or interest. 
 - Every 2 levels - 30,000xp you will unlock a new metallic colour. These are very shiny to give your building a premium look.
 - Every 3 levels - 45,000xp unlocks an emmisive material. This material does not react to shadows and gives your building a nice glow.
 - Every 4 levels - 60,000xp unlocks a gradient material. These look best on the tall buildings, giving a bit more variety and life.
 - Every 5 levels - 75,000xp will unlock a fancy/textured material. These range from bricks to water, from camo to lava. Some of these are animated. All will bring a really fun vibe to you're university.

### Models
Each building has at least 3 models, some have 4. New users will all have the same model for each subject. As the user gains more xp from studying, their chosen subject (selected on the timer screen) will gain specific amounts of xp. The xp earned here will contribute to unlocking new building designs. 90,000xp per subject is required to unlock a new building

A good example of the variation is the Physics and Maths building.  
1. Every user would begin with an observatory style building
2. After earning 90k xp, you get a building made up of 3D shapes such as spheres and cubes
3. Next you get an abstract geometric building made up of floating cubes
4. Finally you can unlock the space shuttle (discovery) ready to launch on its pad.

The subjects users are interested in will be very clearly visible from the models of their buildings and the materials that can be applied to them. If someone were to unlock the space shuttle, that would mean they spent approxamtley 75 hours on Physics and Mathematics. Don't foget you can set the timer whilst you are revising, attending classes or anything else that contributes to your learning.

## Personal User Building
This building will be made up of 4 towers and the user will be able to customise the wall and window colours for each as well as the height of each tower. The materials available will be the same as the subject specific buildings and will be unlocked at the same rate as the rest of the app. The tower heights will be changeable by the user, as will the shape of the tower. This allows the user to establish their own style right at the beginning of the game. <!--The heights of the tower will be limited based on their xp. The more xp someone has, the higher they can set their towers.-->

---

<!--
# Progress Report - Updated 127/01/2021
## Completed components
 - Ability to move, pan and zoom around an enclosed area
 - Ability to customise
   - Change the primary and secondary colours of each building
   - Change between object models
   - Changeable from a customisation menu.
 - Created a polished user interface
 - Created a working backend where data is stored and can be accessed using CRUD requests.
 - Menu background video and music

## Work in progress
 - Polish the move and rotate mechanics
 - read Database in Unity
 - Create tests for the database and unity project

## Asset progress
 - `Blender Models/` 22/26 Building models
   - 3/4 Physics and Maths
   - 2/3 Computer Science
   - 1/3 Art
   - 3/3 Biology and Chemistry
   - 3/3 Law and Politics
   - 3/3 Languages
   - 4/4 Geography and History
   - 3/3 Engineering
 - `Icons/`
   - All currently required assets have been created
 - `Tower Building App/Assets/Resources/Materials/`
   - All 44 materials have been created
 - `Tower Building App/Assets/Sounds/`
   - Music for main screen
   - Button click for main screen
   - music for the rest fo the app
   - Button click for the rest of the app

## Plans
There are some features we plan on implementing but that we are not currently working on. 

Something we intend to improve is how users can gain xp. Our original plan was just to use the in-app timer. We now intend to add 2 more ways of earning xp
 1. A big part of the app is the ability to view other peoples worlds. We want people to be able to see what their friends are up to. Along side this we thought a good feature would be the ability to share xp. we intend on having unique codes, maybe distributed every week via email. When a user enters this code in their app, they will earn xp. The code will then be deleted from the database. This code will not be applicable to the user who received it, meaning they have to distribute it to someone else.
 2. Another feature we would like to implement is a *streak multiplier*. Essentially, for each day a user studies in a row, the xp they earn per second/ per minute will be increased. an example of this would be on Day 1 there is no streak, they earn 5xp ever 10 seconds. on Day 2 they will earn 6xp for every 10 seconds, Day 3, 7xp etc. This still needs some refinement but we think this would be a good feature to have.
-->

# Documentation
For information on how we developed this app, check our GitLab Project wiki. It includes:
 - A list of the team of developers, 
 - How to setup a Unity project in GitLab
 - How to port a Unity app to Android
 - The process of modeling the buildings and terrain in Blender
 - The implementation of camera movement for android.
 - The process of implementing the customisation for the buildings and materials.
 - Development of the user interface, menus and other use interaction
 - The technologies involved with the backend
   - Developing the database
   - Testing the database
   - User authentication
   - Deploying the database

