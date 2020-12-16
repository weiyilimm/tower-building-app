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
import org.springframework.stereotype.Service;

import javax.validation.constraints.NotNull;

@Service
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
    

    public boolean validate(long user, long model, long colour, long texture) {
        System.out.println("Entered the validate function");
        System.out.println(userRepository.findById(user));
        if (userRepository.findById(user).isPresent() && modelRepository.findById(model).isPresent() && paneRepository.findById(colour).isPresent() && textureRepository.findById(texture).isPresent()){
            return true;
        }
        return false;
    }
}
