package com.example.towerbuilderspring.service;

import com.example.towerbuilderspring.repository.ModelRepository;
import com.example.towerbuilderspring.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;


@Service
public class Validator {

    // The master tables to check.
    @Autowired
    UserRepository userRepository;
    @Autowired
    ModelRepository modelRepository;
    

    public boolean validate(long user, long model) {
        System.out.println("Entered the validate function");
        System.out.println(userRepository.findById(user));
        if (userRepository.findById(user).isPresent() && modelRepository.findById(model).isPresent()){
            return true;
        }
        return false;
    }
}
