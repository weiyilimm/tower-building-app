package com.example.towerbuilderspring.service;


import com.example.towerbuilderspring.model.BuildingModels;
import com.example.towerbuilderspring.model.PaneColours;
import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.model.WallTextures;
import com.example.towerbuilderspring.repository.ModelRepository;
import com.example.towerbuilderspring.repository.PaneRepository;
import com.example.towerbuilderspring.repository.TextureRepository;
import com.example.towerbuilderspring.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.validation.constraints.NotNull;

@Component
public class Validator {

    // The master tables to check.
    @Autowired
    UserRepository userRepository;
    @Autowired
    ModelRepository modelRepository;
    @Autowired
    PaneRepository paneRepository;
    @Autowired
    TextureRepository textureRepository;

    public boolean validate(Users user, BuildingModels model, PaneColours colour, WallTextures texture) {
        if (userRepository.findById(user.getUserId()).isPresent() && modelRepository.findById(model.getBuildingCode()).isPresent() && paneRepository.findById(colour.getColourCode()).isPresent() && textureRepository.findById(texture.getWallCode()).isPresent()){
            return true;
        }
        return false;
    }
}
