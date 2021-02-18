package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.BuildingModels;
import com.example.towerbuilderspring.model.UserModelId;
import com.example.towerbuilderspring.model.UserModels;
import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.repository.ModelRepository;
import com.example.towerbuilderspring.repository.UserModelRepository;
import com.example.towerbuilderspring.repository.UserRepository;
import com.example.towerbuilderspring.service.BuildingRequestValid;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;


import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import java.lang.reflect.Array;
import java.util.*;

@RestController
@RequestMapping("/api")
public class UserController {

    //Todo Convert field injections in constructor injections.
    @Autowired
    ModelRepository modelRepository;

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

    @GetMapping("/Users/{id}")
    public ResponseEntity<Users> getUser(@PathVariable UUID id) {
        try {
            Users user = userRepository.findById(id).get();
            return new ResponseEntity<>(user, HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }


    @PostMapping("/Users/")
    public ResponseEntity<Users> createUser(@RequestBody Users user) {
        try {
            Users newUser = new Users(user.getUserName(), user.getEmail(), user.getPassword(), user.getTotalExp());
            userRepository.save(newUser);
            return new ResponseEntity<>(newUser, HttpStatus.CREATED);
        }
        catch (Exception e) {
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
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

        Optional<Users> fetched_user = userRepository.findById(id);
        List<UserModels> all_models = userModelRepository.findAll();
        Set<UserModels> userModels = new HashSet<>();

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

            /**
            Formatting the values to match the frontend model.
             **/

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

                /* TESTING
                System.out.println(currentModel.getUserModelId().getFk_building().getBuildingCode());
                System.out.println(currentModel.getUserModelId().getFk_building().getBuildingName());
                System.out.println(currentModel.getUserModelId().getFk_building().getModelGroup());
                System.out.println(currentModel.getBuilding_xp());
                System.out.println(currentModel.getPrimaryColour());
                System.out.println(currentModel.getSecondaryColour());
                System.out.println(currentModel.getHeight());
                 */

                // Don't use clear() apparently.
                HashMap<String, Object> tempModelData = new HashMap<>();

                tempModelData.put("buildingCode", currentModel.getUserModelId().getFk_building().getBuildingCode());
                tempModelData.put("buildingName", currentModel.getUserModelId().getFk_building().getBuildingName());
                tempModelData.put("buildingGroup", currentModel.getUserModelId().getFk_building().getModelGroup());
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
    }

//    @PostMapping("/Users/{id}/Buildings")
//    public ResponseEntity<List<Object>> addUserBuilding(@PathVariable("id") UUID id,
//                                                          @RequestBody BuildingModels building) {
//        try {
//            Optional<Users> fetched_user = userRepository.findById(id);
//            if (fetched_user.isPresent()) {
//                Users user = fetched_user.get();
//                // TODO Create new validator service here.
//                user.addUserBuilding(building);
//                List<Object> userAndBuildingAdded = Arrays.asList(user, building);
//                return new ResponseEntity<>(userAndBuildingAdded, HttpStatus.OK);
//
//            } else {
//                return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
//            }
//        } catch (Exception e) {
//            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }

    /**
     Implemented updating a User's building as a Post instead of a Put because we're updating a set
     within the user repository rather then a user (which would be directly modifiable through the
     repository interface) itself.
    **/
    @PostMapping("/Users/{userId}/Buildings/{buildingId}")
    public ResponseEntity<UserModels> updateUserBuilding(@PathVariable("userId") UUID userId,
                                                           @PathVariable("buildingId") long buildingId,
                                                           @RequestBody String newUserModel) throws ParseException {

        Optional<Users> fetched_user = userRepository.findById(userId);
        Optional<BuildingModels> fetched_model = modelRepository.findById(buildingId);
        List<UserModels> all_models = userModelRepository.findAll();

        UserModels modelToAdd = new UserModels();
        UserModelId modelId = new UserModelId();

        // Make sure the model id given is matches the model id in the body.
        if (buildingId != fetched_model.get().getBuildingCode()) {
            return new ResponseEntity<>(null, HttpStatus.FORBIDDEN);
        }

        // Make sure that both the user and the building exist.
        if (fetched_user.isPresent() && fetched_model.isPresent()) {

            // Manually parsing Json.
            JSONParser parser = new JSONParser();
            JSONObject json = (JSONObject) parser.parse(newUserModel);

            // Create the Composite key
            modelId.setFk_building(fetched_model.get());
            modelId.setFk_user(fetched_user.get());

            modelToAdd.setUserModelId(modelId);

            // Set the rest of the attributes.
            modelToAdd.setBuilding_xp((((Long) json.get("building_xp")).intValue()));
            modelToAdd.setHeight((((Long) json.get("height")).intValue()));
            modelToAdd.setModelGroup((Long) json.get("modelGroup"));
            modelToAdd.setPrimaryColour((((Long) json.get("primaryColour")).intValue()));
            modelToAdd.setSecondaryColour((((Long) json.get("secondaryColour")).intValue()));

            Users user = fetched_user.get();
            BuildingModels newModel = fetched_model.get();
            // Get the group to be updated.
            long modelGroup = modelToAdd.getModelGroup();

            System.out.println("search started");

            // Search variables
            UserModels currentModel;
            Users currentUser;
            BuildingModels currentBuilding;
            // TODO See if you can make a method in the User Models Repository to do this instead.
            // Go through all the global models and find the one belonging both to the same group and the same user.
            for (int i = 0; i < all_models.size(); i++) {
                currentModel = all_models.get(i);
                currentUser = currentModel.getUserModelId().getFk_user();
                currentBuilding = currentModel.getUserModelId().getFk_building();


                if (currentUser.getId() == userId &&
                        currentBuilding.getModelGroup() == modelGroup) {
                    System.out.println();
                    UserModelId modelToDelete = new UserModelId(currentUser, currentBuilding);
                    System.out.println(modelToAdd.toString());
                    System.out.println();

                    userModelRepository.deleteById(modelToDelete);
                    userModelRepository.save(modelToAdd);

                    return new ResponseEntity<>(modelToAdd, HttpStatus.OK);
                }
            }
            // The user doesn't have any models belonging to that group yet (i.e. initialising the user).
            userModelRepository.save(modelToAdd);
            return new ResponseEntity<>(modelToAdd, HttpStatus.CREATED);
        }
        return new ResponseEntity<>(modelToAdd, HttpStatus.INTERNAL_SERVER_ERROR);
    }

//    @PostMapping("/Users/{userId}/Buildings/{buildingId}")
//    public ResponseEntity<Set<BuildingModels>> changeUserBuilding(@PathVariable("userId") UUID userId,
//                                                           @PathVariable("buildingId") long buildingId,
//                                                           @RequestBody BuildingModels building) {
//
////        try {
//        Optional<Users> fetchedUser = userRepository.findById(userId);
//        Optional<BuildingModels> fetchedBuilding = modelRepository.findById(buildingId);
//
//        if (fetchedBuilding.isPresent() && fetchedUser.isPresent()) {
//            Users user = fetchedUser.get();
//            BuildingModels buildingCheck = fetchedBuilding.get();
//
//            // Get the group of the building.
//            long group = buildingCheck.getModelGroup();
//
//            System.out.println("Group: " + group);
//            // Look for the building that is currently representing that user in the group.
//            BuildingModels getCurrentUserBuildingModel = user.findByBuildingGroup(group);
//            System.out.println("Models: " + getCurrentUserBuildingModel);
//
//            // User already has a model from that group activated.
//            if (getCurrentUserBuildingModel != null) {
//                user.deleteUserBuilding(buildingId);
//            }
//
//            BuildingModels buildingToAdd = new BuildingModels(building.getBuildingCode(), building.getBuildingName(),
//                    building.getModelGroup());
//
//            System.out.println(buildingToAdd);
//
//            // Update the user repository to reflect the new model.
//            user.getUserBuildings().add(buildingToAdd);
//            System.out.println("Before saving: " + user.getUserBuildings());
//            userRepository.save(user);
//            System.out.println("After saving: " + user.getUserBuildings());
//
//            return new ResponseEntity<>(user.getUserBuildings(), HttpStatus.OK);
//            // Find the current user model of the same group and replace it.
//        }
//        else {
//            return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
//        }
//    }
//
//            BuildingRequestValid validator = new BuildingRequestValid();
//            List<Object> result = validator.validate(userId, buildingId);
//            String result = {null};
//
//            System.out.println("The result was " + result);
//            if (result != null) {
//                Users user = (Users) result.get(0);
//                BuildingModels buildingToAdd = (BuildingModels) result.get(1);
//                // Get the group of the building.
//                long group = buildingToAdd.getModelGroup();
//
//                // Look for the building that is currently representing that user in the group.
//                BuildingModels getCurrentUserBuildingModel = user.findByBuildingGroup(group);
//
//                // User already has a model from that group activated.
//                if (getCurrentUserBuildingModel != null) {
//                    user.deleteUserBuilding(buildingId);
//                }
//
//                // Update the user repository to reflect the new model.
//                user.getUserBuildings().add(buildingToAdd);
//                userRepository.save(user);
//                return new ResponseEntity<>(null, HttpStatus.OK);
//
//                // Find the current user model of the same group and replace it.
//            } else {
//                return new ResponseEntity<>(null, HttpStatus.FORBIDDEN);
//            }
//        } catch (Exception e) {
//            System.out.println("Something else");
//            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
//        }

}
