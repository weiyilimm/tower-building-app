package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.*;
import com.example.towerbuilderspring.repository.UserModelRepository;
import com.example.towerbuilderspring.repository.UserRepository;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import org.json.simple.JSONObject;
import java.util.*;

@RestController
@RequestMapping("/api")
public class UserController {

    /***
     *      What is: @Autowired?
     *      The @Autowired annotation will link the Repository (and hence by extension the database) to a variable.
     *      Then using this variable, you can directly access and modify the database using inbuilt methods.
     */
    @Autowired
    UserRepository userRepository;  // This will access the user table.

    @Autowired
    UserModelRepository userModelRepository; // This will access the user model table.

    /***
     *      This function is used to get a list of all the users in the database. This is primarily used to populate the
     *      leaderboard.
     *
     *
     *      What is: @GetMapping?
     *
     *      The @GetMapping annotation means that in order to call this function you must use the URL inside this function.
     *      Some functions (such as getUserBuildings) need dynamic url's to access specific objects. That means that there will be a variable inside
     *      the actual url (usually something along the lines of {id}). This can then be read in using @PathVariable.
     *      (https://www.baeldung.com/spring-pathvariable <- A nice example).
     *
     * @return
     */
    @GetMapping("/Users/")
    public ResponseEntity<List<Users>> getAllUsers() {
        try {
            // Create a new empty list to store all the users.
            List<Users> users = new ArrayList<Users>();
            // Use the repository method .findall() to get all the users. Here I've also added them to the list
            // at the same time using a lambda function.
            userRepository.findAll().forEach(users::add);

            // If there were users in the database then return the users along with a HTTP 200.
            if (!users.isEmpty()) {
                return new ResponseEntity<>(users, HttpStatus.OK);
            } else {
                // Else return nothing with HTTP 204
                return new ResponseEntity<>(HttpStatus.NO_CONTENT);
            }
            // If there was any exception return nothing and display HTTP 500 on the server side console.
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    /***
     *  This Function is used to update the Properties of the User themselves such as their email id and totalEXP.
     *
     *
     * @param id    This is the UUID of the user sent to the database. It is sent over as a string from unity but treated
     *              as a UUID class variable once it enters springboot.
     * @param user  @Request body will take the POJO object sent in the PUT request body and automatically parse it into
     *              an instance of the Users Class. (https://springbootdev.com/2017/04/12/spring-mvc-what-is-requestbody-and-responsebody/)
     *
     *              If this seems difficult to understand at first, there are examples further below and in the
     *              UserLoginController Class where I manually read in the JSON and parse it. I would recommend having a
     *              look and possibly starting with that until your comfortable with POJO.
     * @return
     */
    @PutMapping("/Users/{id}")
    public ResponseEntity<Users> updateUser(@PathVariable("id") UUID id, @RequestBody Users user) {
        // This will search the database using the primary key id and return a user. If the user does not exist it will
        // store null inside Optional.
        Optional<Users> userData = userRepository.findById(id);

        // Checking if there is an object or null.
        if (userData.isPresent()) {
            // .get will take the User class out of optional to allow us to use it. Think indexing a list -> [1].get(0) = 1
            Users user_to_update = userData.get();
            // Once we have the user, update it's values with the ones sent from unity.
            user_to_update.setEmail(user.getEmail());
            user_to_update.setTotalExp(user.getTotalExp());

            return new ResponseEntity<>(userRepository.save(user_to_update), HttpStatus.OK);
        }
        else {
            return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
        }
    }

    /**
      This is used to find and delete a user PERMANENTLY from the database. However for safety's sake it is my recommendation
      that one deletes a users by directly deleting them from the database themselves (using a tool such as PgAdmin).
     **/
    @DeleteMapping("/Users/{id}")
    public ResponseEntity<Users> deleteUser(@PathVariable("id") UUID id) {
        try {
            userRepository.deleteById(id);
            return new ResponseEntity<>(HttpStatus.ACCEPTED);
        }
        catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }


    /**
       Get all buildings belonging to the user requesting them. This function is called at the start and is used
       to tell unity what buildings (and what customizations) to populate the main screen with.
     **/
    @GetMapping("Users/{id}/Buildings")
    public ResponseEntity<HashMap<String, Object>> getUserBuildings(@PathVariable("id") UUID id) {
        try {
            // Get the user (if they exist).
            Optional<Users> fetched_user = userRepository.findById(id);
            // Get ALL the models in the database
            List<UserModels> all_models = userModelRepository.findAll();
            // Create a Dictionary to store the models that belong to the users.
            Set<UserModels> userModels = new HashSet<>();

            if (fetched_user.isPresent()) {
                Users requestedUser = fetched_user.get();

                // Currently manually going through the list as the key UserModels is mapped to a composite key.
                for (Iterator<UserModels> it = all_models.iterator(); it.hasNext();) {
                    UserModels currentModel = it.next();
                    // If the model your looking at has a the users id then it belongs to the user, so add it to the dictionary.
                    if (currentModel.getUserModelId().getFk_user().getId() == id) {
                        userModels.add(currentModel);
                    }
                }


                /**
                        The following code creates a Json object to send back to unity. This allows me more flexibility
                        in data transfer, rather then being forced to send an actual object.
                 **/

                // MANUALLY CREATING JSON OBJECT TO MATCH FRONTEND MODEL

                HashMap<String, Object> formattedUserModels = new HashMap<>();
                List<HashMap<String, Object>> modelsList = new ArrayList<HashMap<String, Object>>();


                // User Details
                formattedUserModels.put("id", id.toString());
                formattedUserModels.put("userName", requestedUser.getUserName());
                formattedUserModels.put("password", requestedUser.getPassword());
                formattedUserModels.put("totalExp", requestedUser.getTotalExp());


                // Loop through all their models and convert them into a list of JSON objects
                for (Iterator<UserModels> it = userModels.iterator(); it.hasNext();) {
                    UserModels currentModel = it.next();


                    // Don't use clear() apparently. Instead just create a new instance of the class.
                    HashMap<String, Object> tempModelData = new HashMap<>();

                    tempModelData.put("buildingCode", currentModel.getUserModelId().getModel());

                    // As we're decoupling we won't store the name in the database, that relation can be made in Unity.
                    //    tempModelData.put("buildingName", currentModel);

                    tempModelData.put("buildingGroup", currentModel.getModelGroup());
                    tempModelData.put("building_xp", currentModel.getBuilding_xp());
                    tempModelData.put("primaryColour", currentModel.getPrimaryColour());
                    tempModelData.put("secondaryColour", currentModel.getSecondaryColour());
                    tempModelData.put("height", currentModel.getHeight());

                    // Add the data to the list.
                    modelsList.add(tempModelData);
                }

                // Store the populated list into the JSON
                formattedUserModels.put("userBuildings", modelsList);
                return new ResponseEntity<>(formattedUserModels, HttpStatus.OK);
            }
            return new ResponseEntity<>(null, HttpStatus.NO_CONTENT);

        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    /**
     This function is used to update the data of a users table. This is called when somebody changes a property of their
     building like material, height, xp ect. It is also used to replace the building with another building of the same
     group e.g. replacing the Burj Khalifa with the Crane building (Engineering).


     Implemented updating a User's building as a Post instead of a Put because we're updating a set
     within the user repository rather then a user (which would be directly modifiable through the
     repository interface) itself.

     Note the check if the model belongs to the correct model group is done in the front end.
     **/
    @PostMapping("/Users/{userId}/Buildings/{buildingId}/{group}")
    public ResponseEntity<UserModels> updateUserBuilding(@PathVariable("userId") UUID userId,
                                                         @PathVariable("buildingId") long buildingId,
                                                         @PathVariable("group") long group,
                                                         @RequestBody String newUserModel) {
        try {
            // Once again find the specific building and get a list of all buildings in the database
            Optional<Users> fetched_user = userRepository.findById(userId);
            List<UserModels> all_models = userModelRepository.findAll();

            /**
                We will be creating a new model with the updated properties to replace the old one.
             **/
            UserModels modelToAdd = new UserModels();
            // Need to create the new composite key here.
            UserModelId modelId = new UserModelId();

            // Make sure the user exists.
            if (fetched_user.isPresent()) {

                // Manually parsing Json.
                JSONParser parser = new JSONParser();
                JSONObject json = (JSONObject) parser.parse(newUserModel);

                // Create the Composite key
                modelId.setModel(buildingId);
                modelId.setFk_user(fetched_user.get());

                modelToAdd.setUserModelId(modelId);

                // Set the rest of the attributes.
                modelToAdd.setBuilding_xp((((Long) json.get("building_xp")).intValue()));
                modelToAdd.setHeight((((Long) json.get("height")).intValue()));
                modelToAdd.setModelGroup(group);
                modelToAdd.setPrimaryColour((((Long) json.get("primaryColour")).intValue()));
                modelToAdd.setSecondaryColour((((Long) json.get("secondaryColour")).intValue()));

                System.out.println("search started");

                // Search variables
                UserModels currentModel;
                Users currentUser;
                long currentBuildingGroup;
                // Go through all the global models and find the one belonging both to the same group and the same user.
                // That is, find the model to update.
                for (int i = 0; i < all_models.size(); i++) {

                    // Every userModel has a composite key composing of a model and a user. Each user has a only one building
                    // of that model group at a time (so only one Science building at any one time). So the building we
                    // want to update will the building with the same user and the same model group as the one sent from
                    // unity.
                    currentModel = all_models.get(i);
                    currentUser = currentModel.getUserModelId().getFk_user();
                    currentBuildingGroup = currentModel.getModelGroup();

                    // If the user already has a building of belonging to that model group.
                    if (currentUser.getId() == userId &&
                            currentBuildingGroup == group) {
                        System.out.println();
                        UserModelId modelToDelete = new UserModelId(currentUser, currentModel.getUserModelId().getModel());

                        System.out.println(currentUser);
                        System.out.println(currentBuildingGroup);
                        System.out.println(modelToAdd);

                        // Delete the old model.
                        userModelRepository.deleteById(modelToDelete);
                        // And update it with the new one.
                        userModelRepository.save(modelToAdd);

                        return new ResponseEntity<>(modelToAdd, HttpStatus.OK);
                    }
                }

                System.out.println("Creating a new model");
                // The user doesn't have any models belonging to that group yet (i.e. initialising the user).
                userModelRepository.save(modelToAdd);
                return new ResponseEntity<>(modelToAdd, HttpStatus.CREATED);
            }
            return new ResponseEntity<>(modelToAdd, HttpStatus.NOT_FOUND);

        } catch (ParseException p) {
            System.out.println("The Json is inputted incorrectly");
            p.printStackTrace();
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }

    }



}
