## Professional Software Development Project
##### Group CS17 - Tower Building App

# **UNI BUILDER**

[[_TOC_]]

# Overview
**Uni Builder** is an app made for Android and IOS in Unity. The app is designed to promote studying and socialising at university. We do this by allowing students to customise the department buildings of a small university. Several buildings are included, each with the option for the user to create a unique colour pallette and design. Users are able to customise each building with a primary and secondary material from a range of over 40 colours and textures.

Students {will be} able to earn xp by sharing codes with friends and/or timing their study sessions. The xp gathered will contribute to the users global xp count. This can be compared on a leader board with other users. Furthermore, each department building will have their own personal xp count. As users studies for specific subjects, they will gain more xp for that specific subject, unlocking a variety of building models.

When we say the app is also designed to promote socialising, students will be able to follow each other as friends and view each others worlds. Something that will make each world especially unique will be the centerpiece of their university, their own custom building.

# Methods of gaining xp
Users will be able to gain xp through an in-app timer. This timer will run for a maximum of 60 minutes. The longer the timer runs, the more xp will go towards their global xp and their subject specific xp. On the timer screen the user is able to select which subject they plan to study for in order to direct the subject specific xp accordingly.

# Customisation
## Subject Specific Buildings
### Materials
Buildings will each have 2 main materials; Primary and Secondary. Depending on the global xp of the user, a different number of materials will be available for their buildings. A new material will be unlocked at milestones. 

*Subject to change:*

Each milestone/level is {500xp}. Every level you will unlock a new matte colour. A colour with not much reflectivity or interest. 

Every 3 levels you will unlock a new metallic colour and a new emissive colour in turn. Metallic colours are shiny and reflective and give a much more premium finish to the buildings. Emissive colours do not react to shadows and work well as a nice glowing accent to any building.

Every 6 levels you will unlock a new special material. Fancy materials include gradients (consisting of 2 or more colours from top to bottom) or textured materials. These include a digital grid, stone bricks, water or even lava.

### Models
Each building has at least 3 models, some have 4. At the beginning of the game everyone will have the same models to start with. As the user gains more xp from studying, their chosen subject (selected on the timer screen) will gain specific amounts of xp. The xp earned here will contribute to unlocking new building designs.

A good example of the variation is the Physics and Maths building.  
1. Every user would begin with an observatory style building
2. After earning a certain amount of xp, you get a building made up of 3D shapes such as spheres and cubes
3. Next you get an abstract geometric building made up of floating cubes
4. Finally you can unlock the space shuttle (discovery) ready to launch on its pad.

The subjects users are interested in will be very clearly visible from the models of their buildings and the materials that can be applied to them.

## Personal User Building
This building will be made up of 4 towers and the user will be able to customise the wall and window colours for each as well as the height of each tower. The materials available will be the same as the subject specific buildings and will be unlocked at the same rate as the rest of the app. and the tower heights will be changeable by the user, as will the shape of the tower. This allows the user to establish something they want right at the beginning of the game. The heights of the tower will be limited based on their xp. The more xp someone has, the higher they can set their towers.

*Subject to change*  
We may have a balancing scale, where the heights of the towers will need to be balanced out. If the user wants each of the 4 towers the same height, it would be smaller than putting all the height into 1 tower (and essentially having a skinnier building).

---

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

# Documentation
For information on how we developed this app, check our GitLab Project wiki. It includes:
 - a list of the team of developers, 
 - how to setup a Unity project in GitLab
 - how to port a Unity app to Android
 - the process of modeling the buildings and terrain in Blender

 *If not included yet, the GitLab Project wiki will also explain:*
 - The backend: What utilities and frameworks we used
 - How we developed specific components of the game including:
   - The user interface, menus, and user interaction
   - Camera movement and rotation
   - Customisation

