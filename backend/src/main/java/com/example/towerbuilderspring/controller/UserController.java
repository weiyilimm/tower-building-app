package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.*;
import com.example.towerbuilderspring.repository.UserModelRepository;
import com.example.towerbuilderspring.repository.UserModelRepositoryDecoupled;
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

//    @GetMapping("/Users/{id}")
//    public ResponseEntity<Users> getUser(@PathVariable UUID id) {
//        try {
//            Users user = userRepository.findById(id).get();
//            return new ResponseEntity<>(user, HttpStatus.OK);
//        } catch (Exception e) {
//            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
//        }
//    }
//
//
//    @PostMapping("/Users/")
//    public ResponseEntity<Users> createUser(@RequestBody Users user) {
//        try {
//            Users newUser = new Users(user.getUserName(), user.getEmail(), user.getPassword(), user.getTotalExp());
//            userRepository.save(newUser);
//            return new ResponseEntity<>(newUser, HttpStatus.CREATED);
//        }
//        catch (Exception e) {
//            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }

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

//    @GetMapping("Users/{id}/Buildings")
//    public ResponseEntity<HashMap<String, Object>> getUserBuildings(@PathVariable("id") UUID id) {
//
//        Optional<Users> fetched_user = userRepository.findById(id);
//        List<UserModels> all_models = userModelRepository.findAll();
//        Set<UserModels> userModels = new HashSet<>();
//
//        if (fetched_user.isPresent()) {
//            Users requestedUser = fetched_user.get();
//
//            // Currently manually going through the list as the key UserModels is mapped to a composite key.
//            for (Iterator<UserModels> it = all_models.iterator(); it.hasNext();) {
//                UserModels currentModel = it.next();
//                // TODO clean this up.
//                if (currentModel.getUserModelId().getFk_user().getId() == id) {
//                    userModels.add(currentModel);
//                }
//            }
//
//            /**
//            Formatting the values to match the frontend model.
//             **/
//
//            // TODO look if you can pass only strings i.e. HashMap<String, String> instead of HashMap<String, Object>.
//            HashMap<String, Object> formattedUserModels = new HashMap<>();
//            List<HashMap<String, Object>> modelsList = new ArrayList<HashMap<String, Object>>();
//
//
//                // User Details
//            formattedUserModels.put("id", id.toString());
//            formattedUserModels.put("userName", requestedUser.getUserName());
//            formattedUserModels.put("password", requestedUser.getPassword());
//            formattedUserModels.put("totalExp", requestedUser.getTotalExp());
//                // Their current models
//            for (Iterator<UserModels> it = userModels.iterator(); it.hasNext();) {
//                UserModels currentModel = it.next();
//
//
//                // Don't use clear() apparently.
//                HashMap<String, Object> tempModelData = new HashMap<>();
//
//                tempModelData.put("buildingCode", currentModel.getUserModelId().getFk_building().getBuildingCode());
//                tempModelData.put("buildingName", currentModel.getUserModelId().getFk_building().getBuildingName());
//                tempModelData.put("buildingGroup", currentModel.getUserModelId().getFk_building().getModelGroup());
//                tempModelData.put("building_xp", currentModel.getBuilding_xp());
//                tempModelData.put("primaryColour", currentModel.getPrimaryColour());
//                tempModelData.put("secondaryColour", currentModel.getSecondaryColour());
//                tempModelData.put("height", currentModel.getHeight());
//
//
//
//                // Add the data to the list.
//                modelsList.add(tempModelData);
//            }
//
//            formattedUserModels.put("userBuildings", modelsList);
//            return new ResponseEntity<>(formattedUserModels, HttpStatus.OK);
//        }
//        return new ResponseEntity<>(null, HttpStatus.NO_CONTENT);
//    }
//
//    /**
//     Implemented updating a User's building as a Post instead of a Put because we're updating a set
//     within the user repository rather then a user (which would be directly modifiable through the
//     repository interface) itself.
//    **/
//    @PostMapping("/Users/{userId}/Buildings/{buildingId}")
//    public ResponseEntity<UserModels> updateUserBuilding(@PathVariable("userId") UUID userId,
//                                                           @PathVariable("buildingId") long buildingId,
//                                                           @RequestBody String newUserModel) throws ParseException {
//
//        Optional<Users> fetched_user = userRepository.findById(userId);
//        Optional<BuildingModels> fetched_model = modelRepository.findById(buildingId);
//        List<UserModels> all_models = userModelRepository.findAll();
//
//        UserModels modelToAdd = new UserModels();
//        UserModelId modelId = new UserModelId();
//
//        // Make sure the model id given is matches the model id in the body.
//        if (buildingId != fetched_model.get().getBuildingCode()) {
//            return new ResponseEntity<>(null, HttpStatus.FORBIDDEN);
//        }
//
//        System.out.println(userId.toString());
//        System.out.println("cake");
//        System.out.println(fetched_user.isPresent());
//        System.out.println(fetched_model.isPresent());
//
//        System.out.println(userRepository.findAll());
//
//        // Make sure that both the user and the building exist.
//        if (fetched_user.isPresent() && fetched_model.isPresent()) {
//
//            // Manually parsing Json.
//            JSONParser parser = new JSONParser();
//            JSONObject json = (JSONObject) parser.parse(newUserModel);
//
//            // Create the Composite key
//            modelId.setFk_building(fetched_model.get());
//            modelId.setFk_user(fetched_user.get());
//
//            modelToAdd.setUserModelId(modelId);
//
//            // Set the rest of the attributes.
//            modelToAdd.setBuilding_xp((((Long) json.get("building_xp")).intValue()));
//            modelToAdd.setHeight((((Long) json.get("height")).intValue()));
//            modelToAdd.setModelGroup((Long) json.get("modelGroup"));
//            modelToAdd.setPrimaryColour((((Long) json.get("primaryColour")).intValue()));
//            modelToAdd.setSecondaryColour((((Long) json.get("secondaryColour")).intValue()));
//
//            Users user = fetched_user.get();
//            BuildingModels newModel = fetched_model.get();
//            // Get the group to be updated.
//            long modelGroup = modelToAdd.getModelGroup();
//
//            System.out.println("search started");
//
//            // Search variables
//            UserModels currentModel;
//            Users currentUser;
//            BuildingModels currentBuilding;
//            // TODO See if you can make a method in the User Models Repository to do this instead.
//            // Go through all the global models and find the one belonging both to the same group and the same user.
//            for (int i = 0; i < all_models.size(); i++) {
//                currentModel = all_models.get(i);
//                currentUser = currentModel.getUserModelId().getFk_user();
//                currentBuilding = currentModel.getUserModelId().getFk_building();
//
//
//                if (currentUser.getId() == userId &&
//                        currentBuilding.getModelGroup() == modelGroup) {
//                    System.out.println();
//                    UserModelId modelToDelete = new UserModelId(currentUser, currentBuilding);
//                    System.out.println(modelToAdd.toString());
//                    System.out.println();
//
//                    userModelRepository.deleteById(modelToDelete);
//                    userModelRepository.save(modelToAdd);
//
//                    return new ResponseEntity<>(modelToAdd, HttpStatus.OK);
//                }
//            }
//            // The user doesn't have any models belonging to that group yet (i.e. initialising the user).
//            userModelRepository.save(modelToAdd);
//            return new ResponseEntity<>(modelToAdd, HttpStatus.CREATED);
//        }
//        return new ResponseEntity<>(modelToAdd, HttpStatus.INTERNAL_SERVER_ERROR);
//    }
//







//    @GetMapping("/Users/Test/Name/{name}")
//    public ResponseEntity<Users> userNameTest(@PathVariable("name") String name) {
//        Users check = userRepository.findByUserName(name);
//        return new ResponseEntity<>(check, HttpStatus.OK);
//    }

//    @GetMapping("Users/Test/User/{name}/Model/{id}")
//    public ResponseEntity<UserModels> userModelTest(@PathVariable("name") String name,
//                                                    @PathVariable("id") long id) {
//        if (userRepository.findByUserName(name) != null) {
//            return new ResponseEntity<>(userModelRepository.findByUserModelIdAndAndModelGroup())
//        }
//    }


    @Autowired
    UserModelRepositoryDecoupled userModelRepositoryDecoupled;

    @GetMapping("Users/{id}/Buildings")
    public ResponseEntity<HashMap<String, Object>> getUserBuildings(@PathVariable("id") UUID id) {

        Optional<Users> fetched_user = userRepository.findById(id);
        List<UserModelDecoupled> all_models = userModelRepositoryDecoupled.findAll();
        Set<UserModelDecoupled> userModels = new HashSet<>();

        if (fetched_user.isPresent()) {
            Users requestedUser = fetched_user.get();

            // Currently manually going through the list as the key UserModels is mapped to a composite key.
            for (Iterator<UserModelDecoupled> it = all_models.iterator(); it.hasNext();) {
                UserModelDecoupled currentModel = it.next();
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
            for (Iterator<UserModelDecoupled> it = userModels.iterator(); it.hasNext();) {
                UserModelDecoupled currentModel = it.next();


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
    }

    /**
     Implemented updating a User's building as a Post instead of a Put because we're updating a set
     within the user repository rather then a user (which would be directly modifiable through the
     repository interface) itself.

     Note the check if the model belongs to the correct model group will be done in the front end.
     **/
    @PostMapping("/Users/{userId}/Buildings/{buildingId}")
    public ResponseEntity<UserModelDecoupled> updateUserBuilding(@PathVariable("userId") UUID userId,
                                                         @PathVariable("buildingId") long buildingId,
                                                         @PathVariable("group") long group,
                                                         @RequestBody String newUserModel) throws ParseException {

        Optional<Users> fetched_user = userRepository.findById(userId);
        List<UserModelDecoupled> all_models = userModelRepositoryDecoupled.findAll();

        UserModelDecoupled modelToAdd = new UserModelDecoupled();
        UserModelIdDecoupled modelId = new UserModelIdDecoupled();

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
            UserModelDecoupled currentModel;
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
                    UserModelIdDecoupled modelToDelete = new UserModelIdDecoupled(currentUser, currentModel.getUserModelId().getModel());
                    System.out.println("Going to delete a model");

                    System.out.println(currentUser);
                    System.out.println(currentBuildingGroup);
                    System.out.println(modelToAdd);

                    userModelRepositoryDecoupled.deleteById(modelToDelete);
                    userModelRepositoryDecoupled.save(modelToAdd);

                    return new ResponseEntity<>(modelToAdd, HttpStatus.OK);
                }
            }

            System.out.println("Creating a new model");
            // The user doesn't have any models belonging to that group yet (i.e. initialising the user).
            userModelRepositoryDecoupled.save(modelToAdd);
            return new ResponseEntity<>(modelToAdd, HttpStatus.CREATED);
        }
        return new ResponseEntity<>(modelToAdd, HttpStatus.INTERNAL_SERVER_ERROR);
    }

}
