package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.repository.UserModelRepository;
import com.example.towerbuilderspring.repository.UserRepository;
import com.example.towerbuilderspring.service.mail.EmailServiceImpl;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
import org.springframework.beans.factory.annotation.Autowired;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("api/Auth/")
public class UserLoginController {

    @Autowired
    UserRepository userRepository;

    @Autowired
    UserModelRepository userModelRepository;

    private PasswordEncoder encoder = new BCryptPasswordEncoder();

    @PostMapping("Login/")
    public ResponseEntity<Users> authenticateUser(@RequestBody String details) throws ParseException {

       try {
           // Manually parsing Json.
           JSONParser parser = new JSONParser();
           JSONObject json = (JSONObject) parser.parse(details);
           String password = (String) json.get("password");
           String username = (String) json.get("username");

           Users user = userRepository.findByUserName(username);

           if (user != null && encoder.matches(password, user.getPassword())) {
               System.out.println("User was found");
               return new ResponseEntity<Users>(user, HttpStatus.OK);
           } else {
               return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
           }
       } catch(Exception e) {
           System.out.println("There is a error with the data sent");
           return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
       }

    }

    @PostMapping("SignUp/")
    public ResponseEntity<Object> createUser(@RequestBody Users user) {
        try {

            String encryptedPassword = encoder.encode(user.getPassword());
            Users newUser = new Users(user.getUserName(), user.getEmail(), encryptedPassword, user.getTotalExp());
            userRepository.save(newUser);
            return new ResponseEntity<>(user, HttpStatus.CREATED);
        }
        catch (Exception e) {
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    // Todo Once Roles have been successfully implemented make sure only the admin can delete a user.
    @DeleteMapping("Delete/{username}/")
    public ResponseEntity<Users> deleteUser(@PathVariable("username") String username) {
        try {
            Users userToDelete = userRepository.findByUserName(username);

            System.out.println(userToDelete.toString());
            if (userToDelete != null) {
                userRepository.delete(userToDelete);
                return new ResponseEntity<>(userToDelete, HttpStatus.OK);
            }
            else {
                return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
            }
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }


//    // Testing email function
//    @GetMapping("Test/Email")
//    public ResponseEntity<Users> checkEmailSent() {
//        @Autowired
//        EmailServiceImpl emailService;
//    }

}
