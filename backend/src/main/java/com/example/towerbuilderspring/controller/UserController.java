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

//    //Todo Convert field injections in constructor injections.
//    @Autowired
//    ModelRepository modelRepository;

    @Autowired
    UserRepository userRepository;

    @Autowired
    UserModelRepository userModelRepository;

    @GetMapping("/Users/")
    public ResponseEntity<List<Users>> getAllUsers() {
        try {
            List<Users> users = new ArrayList<Users>();
            userRepository.findAll().forEach(users::add);

            if (!users.isEmpty()) {
                return new ResponseEntity<>(users, HttpStatus.OK);
            } else {
                return new ResponseEntity<>(HttpStatus.NO_CONTENT);
            }
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }


    @PutMapping("/Users/{id}")
    public ResponseEntity<Users> updateUser(@PathVariable("id") UUID id, @RequestBody Users user) {
        Optional<Users> userData = userRepository.findById(id);

        if (userData.isPresent()) {
            Users user_to_update = userData.get();
            user_to_update.setEmail(user.getEmail());
            user_to_update.setTotalExp(user.getTotalExp());

            return new ResponseEntity<>(userRepository.save(user_to_update), HttpStatus.OK);
        }
        else {
            return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
        }
    }

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


    @GetMapping("Users/{id}/Buildings")
    public ResponseEntity<HashMap<String, Object>> getUserBuildings(@PathVariable("id") UUID id) {

        try {
            Optional<Users> fetched_user = userRepository.findById(id);
            List<UserModels> all_models = userModelRepository.findAll();
            Set<UserModels> userModels = new HashSet<>();

            System.out.println("Passed fetched_user");

            if (fetched_user.isPresent()) {
                Users requestedUser = fetched_user.get();

                // Currently manually going through the list as the key UserModels is mapped to a composite key.
                for (Iterator<UserModels> it = all_models.iterator(); it.hasNext();) {
                    UserModels currentModel = it.next();
                    // TODO clean this up.
                    if (currentModel.getUserModelId().getFk_user().getId() == id) {
                        userModels.add(currentModel);
                    }
                }

                //Formatting the values to match the frontend model.

                // TODO look if you can pass only strings i.e. HashMap<String, String> instead of HashMap<String, Object>.
                HashMap<String, Object> formattedUserModels = new HashMap<>();
                List<HashMap<String, Object>> modelsList = new ArrayList<HashMap<String, Object>>();


                // User Details
                formattedUserModels.put("id", id.toString());
                formattedUserModels.put("userName", requestedUser.getUserName());
                formattedUserModels.put("password", requestedUser.getPassword());
                formattedUserModels.put("totalExp", requestedUser.getTotalExp());
                // Their current models
                for (Iterator<UserModels> it = userModels.iterator(); it.hasNext();) {
                    UserModels currentModel = it.next();


                    // Don't use clear() apparently.
                    HashMap<String, Object> tempModelData = new HashMap<>();

                    tempModelData.put("buildingCode", currentModel.getUserModelId().getModel());

                    // As we're decoupling we won't store the name in the database, that relation can be made in Unity.
//                tempModelData.put("buildingName", currentModel);

                    tempModelData.put("buildingGroup", currentModel.getModelGroup());
                    tempModelData.put("building_xp", currentModel.getBuilding_xp());
                    tempModelData.put("primaryColour", currentModel.getPrimaryColour());
                    tempModelData.put("secondaryColour", currentModel.getSecondaryColour());
                    tempModelData.put("height", currentModel.getHeight());

                    // Add the data to the list.
                    modelsList.add(tempModelData);
                }

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
            Optional<Users> fetched_user = userRepository.findById(userId);
            List<UserModels> all_models = userModelRepository.findAll();

            UserModels modelToAdd = new UserModels();
            UserModelId modelId = new UserModelId();

            // Make sure that both the user and the building exist.
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
                // TODO See if you can make a method in the User Models Repository to do this instead.
                // Go through all the global models and find the one belonging both to the same group and the same user.
                for (int i = 0; i < all_models.size(); i++) {
                    currentModel = all_models.get(i);
                    currentUser = currentModel.getUserModelId().getFk_user();
                    currentBuildingGroup = currentModel.getModelGroup();

                    System.out.println("Inside the loop");
                    if (currentUser.getId() == userId &&
                            currentBuildingGroup == group) {
                        System.out.println();
                        UserModelId modelToDelete = new UserModelId(currentUser, currentModel.getUserModelId().getModel());
                        System.out.println("Going to delete a model");

                        System.out.println(currentUser);
                        System.out.println(currentBuildingGroup);
                        System.out.println(modelToAdd);

                        userModelRepository.deleteById(modelToDelete);
                        userModelRepository.save(modelToAdd);

                        return new ResponseEntity<>(modelToAdd, HttpStatus.OK);
                    }
                }

                System.out.println("Creating a new model");
                // The user doesn't have any models belonging to that group yet (i.e. initialising the user).
                userModelRepository.save(modelToAdd);
                return new ResponseEntity<>(modelToAdd, HttpStatus.CREATED);
            }
            return new ResponseEntity<>(modelToAdd, HttpStatus.NO_CONTENT);

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
