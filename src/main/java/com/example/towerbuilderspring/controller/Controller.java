package com.example.towerbuilderspring.controller;


import com.example.towerbuilderspring.model.User;
import com.example.towerbuilderspring.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

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

    @GetMapping("/Users/{id}")
    public ResponseEntity<User> getUserById(@PathVariable("id") long id){
        Optional<User> userData = userRepository.findById(id);

        if (userData.isPresent()) {
            return new ResponseEntity<>(userData.get(), HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

//    @GetMapping("/Users/valid")
//    public ResponseEntity<User> getAllValidUsers(){
//        try {
//            List<User> valid_users = userRepository.findByValid(true);
//
//            if (valid_users.isEmpty()){
//                return new ResponseEntity<>(HttpStatus.NO_CONTENT);
//            }
//            return new ResponseEntity<>(valid_users, HttpStatus.OK);
//        }
//        catch (Exception e) {
//            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }

    @PostMapping("/Users/")
    public ResponseEntity<User> createUser(@RequestBody User user) {
        try {
            User new_user = userRepository.save(new User(user.getName(), user.getTowers(), false ));
            return new ResponseEntity<>(new_user, HttpStatus.CREATED);
        }
        catch (Exception e) {
            return new ResponseEntity<>(null,HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @PutMapping("/Users/{id}")
    public ResponseEntity<User> updateUser(@PathVariable("id") long id, @RequestBody User user){
        Optional<User> userData = userRepository.findById(id);

        if (userData.isPresent()){
            User user_to_update = userData.get();
            user_to_update.setName(user.getName());
            user_to_update.setTowers(user.getTowers());
            user_to_update.setValid(user.getValid());

            return new ResponseEntity<>(userRepository.save(user_to_update), HttpStatus.OK);
        }
        else {
            return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
        }
    }

    @DeleteMapping("/Users/")
    public ResponseEntity<User> deleteAllUsers(){
        try {
            userRepository.deleteAll();
            return new ResponseEntity<>(HttpStatus.NO_CONTENT);
        }
        catch (Exception e){
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
    
    @DeleteMapping("/Users/{id}")
    public ResponseEntity<User> deleteUser(@PathVariable("id") long id){
        try{
            userRepository.deleteById(id);
            return new ResponseEntity<>(HttpStatus.NO_CONTENT);
        }
        catch (Exception e){
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

}
