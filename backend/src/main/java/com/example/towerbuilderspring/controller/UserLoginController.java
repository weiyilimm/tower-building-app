package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.repository.UserModelRepository;
import com.example.towerbuilderspring.repository.UserRepository;
import com.example.towerbuilderspring.service.Hashing;
import org.apache.coyote.Response;
import org.junit.experimental.theories.internal.ParameterizedAssertionError;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.autoconfigure.domain.EntityScan;
import org.springframework.context.annotation.Bean;
import org.springframework.data.jpa.repository.config.EnableJpaAuditing;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("api/")
public class UserLoginController {

    @Autowired
    UserRepository userRepository;

    @Autowired
    UserModelRepository userModelRepository;

    private PasswordEncoder encoder = new BCryptPasswordEncoder();

    @GetMapping("Test/login/{userName}/{password}")
    public ResponseEntity<Users>  authenticateUser(@PathVariable String userName,
                                                   @PathVariable String password)  throws AssertionError {
        try
        {
            Users user = userRepository.findByUserName(userName);
            System.out.println(user);

            if (user != null) {
                System.out.println("The user was found");

                System.out.println(encoder.matches(password, user.getPassword()));
                return new ResponseEntity<>(user,HttpStatus.OK);
            }
            throw new AssertionError("The username or password is incorrect");
        }
        catch (AssertionError e) {
            return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
        }
        catch (Exception e) {
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @PostMapping("Test/SignUp/Users/")
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

}
