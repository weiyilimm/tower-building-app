//package com.example.towerbuilderspring.service;
//
//import com.example.towerbuilderspring.model.BuildingModels;
//import com.example.towerbuilderspring.model.Users;
//import com.example.towerbuilderspring.repository.ModelRepository;
//import com.example.towerbuilderspring.repository.UserRepository;
//import org.apache.catalina.User;
//import org.springframework.beans.factory.annotation.Autowired;
//
//import java.util.Arrays;
//import java.util.List;
//import java.util.Optional;
//import java.util.UUID;
//
//
//public class BuildingRequestValid {
//
//    @Autowired
//    UserRepository userRepository;
//    @Autowired
//    ModelRepository modelRepository;
//
//    public List<Object> validate(UUID userId, long buildingId) {
//        System.out.println("Inside Validate");
//        Optional<Users> fetchedUser = userRepository.findById(userId);
//        System.out.println("user passed");
//        Optional<BuildingModels> fetchedBuilding = modelRepository.findById(buildingId);
//
//        System.out.println("Fetched the user and models");
//        System.out.println("user " + fetchedUser + " model " + fetchedBuilding);
//
//        // First check if both exist in there respective tables or not.
//        if (fetchedUser.isPresent() && fetchedBuilding.isPresent()) {
//            System.out.println("Both user and building present");
//            Users user = fetchedUser.get();
//            BuildingModels building = fetchedBuilding.get();
//        }
//        else{
//            System.out.println("User or building not found");
//        }
//        return null;
//    }
//}
