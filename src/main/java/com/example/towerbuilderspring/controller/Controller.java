package com.example.towerbuilderspring.controller;


import com.example.towerbuilderspring.model.User;
import com.example.towerbuilderspring.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;

@CrossOrigin(origins = "http://localhost:8081")
@RestController
@RequestMapping("/api")
public class Controller {

    @Autowired
    UserRepository userRepository;

    @GetMapping("/Users")
    public ResponseEntity<List<User>> getAllUsers(@RequestParam(required = false) String name){
        try {
            List<User> users = new ArrayList<User>();

            if (name == null){
                userRepository.findAll().forEach(users::add);
            }
            else {
                userRepository.findAll().forEach(users::add);
            }

            if (users.isEmpty()) {
                return new ResponseEntity<>(HttpStatus.NO_CONTENT);
            }

            return new ResponseEntity<>(users, HttpStatus.OK);
        }
        catch (Exception e) {
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

}
