package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.util.ArrayList;
import java.util.List;

@RestController
@RequestMapping("/api")
public class Controller {

    @Autowired
    UserRepository userRepository;

    @GetMapping("/Users")
    public ResponseEntity<List<Users>> getAllUsers(@RequestParam(required= false) String name) {
        try {
            List<Users> users = new ArrayList<Users>();

            if (!users.isEmpty()) {
                userRepository.findAll().forEach(users::add);
                return new ResponseEntity<>(HttpStatus.OK);
            } else {
                return new ResponseEntity<>(HttpStatus.NO_CONTENT);
            }
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }

    }

}
