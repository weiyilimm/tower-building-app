package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.UserTowers;
import com.example.towerbuilderspring.repository.*;
import com.example.towerbuilderspring.service.Validator;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;


import java.util.*;

import java.util.ArrayList;
import java.util.List;

@RestController
@RequestMapping("/api")
public class UserTowerController {

    @Autowired
    UserTowerRepository userTowerRepository;

    @Autowired
    Validator check_input;


    // User Tower Table code
    @GetMapping("/UserTowers/")
    public ResponseEntity<List<UserTowers>> getAllUserTowers() {
        try {
            List<UserTowers> userTowers = new ArrayList<UserTowers>();
            userTowerRepository.findAll().forEach(userTowers::add);

            if (!userTowers.isEmpty()) {
                return new ResponseEntity<>(userTowers, HttpStatus.OK);
            } else {
                return new ResponseEntity<>(HttpStatus.NO_CONTENT);
            }
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }


    // When placing data in the database need to check that each value belongs in the respective table.

    @PostMapping("/UserTowers/")
    public ResponseEntity<UserTowers> createUserTower(@RequestBody UserTowers tower) {
        try {
            if (check_input.validate(tower.getUser(), tower.getModels()) == true) {
                System.out.println("Passed the input test");
                UserTowers newTower = new UserTowers(tower.getName(), tower.getUser(), tower.getModels(), tower.getPrimaryColour(), tower.getSecondaryColour());
                userTowerRepository.save(newTower);
                return new ResponseEntity<>(newTower, HttpStatus.CREATED);
            }
            else {
                return new ResponseEntity<>(HttpStatus.FORBIDDEN);
            }
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }


    // This code must also incorporate some backend checks first before changing data.

    @PutMapping("/UserTowers/{id}")
    public ResponseEntity<UserTowers> updateUserTower(@PathVariable("id") long id, @RequestBody UserTowers userTower) {

        Optional<UserTowers> userTowerData = userTowerRepository.findById(id);

        if (userTowerData.isPresent()) {
            if (check_input.validate(userTower.getUser(), userTower.getModels())) {
                UserTowers tower_to_update = userTowerData.get();
                tower_to_update.setTowerId(id);
                tower_to_update.setName(userTower.getName());
                tower_to_update.setUser(userTower.getUser());
                tower_to_update.setModels(userTower.getModels());

                return new ResponseEntity<>(userTowerRepository.save(tower_to_update), HttpStatus.ACCEPTED);
            }
            else {
                return new ResponseEntity<>(null, HttpStatus.FORBIDDEN);
            }
        }
        else {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @DeleteMapping("/UserTowers/{id}")
    public ResponseEntity<UserTowers> deleteUserTower(@PathVariable("id") long id) {

        try {
            Optional<UserTowers> towerToDelete = userTowerRepository.findById(id);
            if (towerToDelete.isPresent()) {
                userTowerRepository.deleteById(id);
                return new ResponseEntity<>(towerToDelete.get(), HttpStatus.ACCEPTED);
            } else {
                return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
            }
        } catch (Exception e) {
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }

    }


}
