package com.example.towerbuilderspring.service;

import com.example.towerbuilderspring.model.BuildingModels;
import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.repository.ModelRepository;
import com.example.towerbuilderspring.repository.UserRepository;
import org.apache.catalina.User;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.Arrays;
import java.util.List;
import java.util.Optional;
import java.util.UUID;

public class BuildingRequestValid {

    @Autowired
    UserRepository userRepository;
    @Autowired
    ModelRepository modelRepository;

    public List<Object> validate(UUID userId, long buildingId) {
        Optional<Users> fetchedUser = userRepository.findById(userId);
        Optional<BuildingModels> fetchedBuilding = modelRepository.findById(buildingId);

        // First check if both exist in there respective tables or not.
        if (fetchedUser.isPresent() && fetchedBuilding.isPresent()) {
            Users user = fetchedUser.get();
            BuildingModels building = fetchedBuilding.get();
            if (user.getUserBuilding(building) != null) {
                List<Object> userAndBuilding = Arrays.asList(user, building);
                return userAndBuilding;
            }
        }
        return null;
    }
}
