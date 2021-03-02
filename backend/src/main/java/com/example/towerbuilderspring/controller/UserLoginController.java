package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.repository.UserModelRepository;
import com.example.towerbuilderspring.repository.UserRepository;
import org.apache.coyote.Response;
import org.junit.experimental.theories.internal.ParameterizedAssertionError;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("api/")
public class UserLoginController {

    @Autowired
    UserRepository userRepository;

    @Autowired
    UserModelRepository userModelRepository;

    @GetMapping("login/{userName}/{password}")
    public ResponseEntity<Users>  authenticateUser(@PathVariable String userName,
                                                   @PathVariable String password)  throws AssertionError {
        try
        {
            Users user = userRepository.findByName(userName);
            if (user == null || !user.getPassword().equals(password)){
                throw new AssertionError("The username or password is incorrect");
            }

            return new ResponseEntity<>(user,HttpStatus.OK);
        }
        catch (AssertionError e) {
            return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
        }
        catch (Exception e) {
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }


}
