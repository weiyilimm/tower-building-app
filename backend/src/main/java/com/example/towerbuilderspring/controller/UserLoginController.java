package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.repository.UserModelRepository;
import com.example.towerbuilderspring.repository.UserRepository;
import com.example.towerbuilderspring.service.mail.EmailServiceImpl;
import com.example.towerbuilderspring.service.security.OTPHandler;
import javassist.NotFoundException;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
import org.springframework.beans.factory.annotation.Autowired;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.client.HttpClientErrorException;

import javax.naming.AuthenticationException;

@RestController
@RequestMapping("api/Auth/")
public class UserLoginController {

    // Connect to the tables
    @Autowired
    UserRepository userRepository;

    @Autowired
    UserModelRepository userModelRepository;


    // Connect to services.
    @Autowired
    EmailServiceImpl emailService;

    @Autowired
    OTPHandler otpHandler;

    // This is used to take care of the encryption related tasks.
    private PasswordEncoder encoder = new BCryptPasswordEncoder();

    /**
     *  This function will handle user login. The username and password will be sent within the request body of the
     *  POST request, which when received will be used to perform authentication on the server side (which is here)
     *
     * @param details: This is JSON data sent from Unity which will be parsed.
     * @return
     */
    @PostMapping("Login/")
    public ResponseEntity<Users> authenticateUser(@RequestBody String details){

       try {
           // We will convert the data into a proper Json class.
           JSONParser parser = new JSONParser();
           JSONObject json = (JSONObject) parser.parse(details);
           // The keys (password, username) are case sensitive.
           String password = (String) json.get("password");
           String username = (String) json.get("username");

           Users user = userRepository.findByUserName(username);

           // Here we are comparing the encrypted versions of the sent password and stored one. If these match
           // (and the users exists) the return the user to be stored in the app for the remainder of the session.
           if (user != null && encoder.matches(password, user.getPassword())) {
               System.out.println("User was found");
               return new ResponseEntity<Users>(user, HttpStatus.OK);
           } else {
               return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
           }
           // This means the JSON that was sent was incorrect.
       } catch(ParseException e) {
           System.out.println("Data parse error");
           e.printStackTrace();
           return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
       } catch (Exception e) {
           e.printStackTrace();
           return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
       }

    }

    /**
     *  This handles user registration.
     * @param user: The data will be converted into a User class. This means that the keys in the POST Request.body
     *            MUST match those in the User class.
     * @return
     */
    @PostMapping("SignUp/")
    public ResponseEntity<Object> createUser(@RequestBody Users user) {
        try {
            // The users password will be encrypted before sending data to the database.
            String encryptedPassword = encoder.encode(user.getPassword());
            // Create a new instance of the user (with the encrypted password) to store into the database.
            Users newUser = new Users(user.getUserName(), user.getEmail(), encryptedPassword, user.getTotalExp());
            userRepository.save(newUser);
            return new ResponseEntity<>(user, HttpStatus.CREATED);
        }
        catch (Exception e) {
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    /**
     *  This has not been implemented in the unity side. So currently if you want to delete the user
     *  , you must delete the user from the database directly.
     *
     *  This function should be ready for implementation as it has passed all the unit tests.
     */
    @DeleteMapping("Delete/{username}/")
    public ResponseEntity<Users> deleteUser(@PathVariable("username") String username) {
        try {
            Users userToDelete = userRepository.findByUserName(username);

            // Make a check if null was sent.

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


    /**
     *  This will create the email and send it to the email specified in the Request.BODY.
     * @param data
     */
    @PostMapping("Email/")
    public ResponseEntity<String> emailOTP(@RequestBody String data) {

        try {

            JSONParser parser = new JSONParser();
            JSONObject json = (JSONObject) parser.parse(data);
            String email = (String) json.get("email");

            // Generate the OTP
            String OTP = otpHandler.generateOTP();
            otpHandler.saveOTP(email, OTP);

            // This will be the message in the email.
            String message = String.format("Enter this OTP: %s  to reset your password", OTP);
            // Currently sending a text email.
            emailService.sendSimpleMessage(email, "This is from Tower Builder", message);
            return new ResponseEntity<>(null , HttpStatus.OK);

        } catch (ParseException p) {
            System.out.println("The JSON was incorrectly formatted");
            p.printStackTrace();
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);

        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }

    }


    /**
     *  Most of the processing done in this function is performed in the OTPHandler class. This exists to connect that
     *  service to the url.
     * @param data
     */
    @PostMapping("validateOTP/")
    public ResponseEntity<String> validateOTP(@RequestBody String data) {
        try {
            JSONParser parser = new JSONParser();
            JSONObject json = (JSONObject) parser.parse(data);

            String email = (String) json.get("email");
            String OTP = (String) json.get("OTP");

            // If the otpHandler is unable to validate the OTP it will throw one of the 4 exceptions stated below.
            Users user = otpHandler.validateOTP(email, OTP);
            return new ResponseEntity<>(null, HttpStatus.OK);

        } catch (ParseException p) {
            p.printStackTrace();
            return new ResponseEntity<>(null, HttpStatus.BAD_REQUEST);

        } catch (NotFoundException n) {
            n.printStackTrace();
            return new ResponseEntity<>(null, HttpStatus.UNAUTHORIZED);

        } catch(AuthenticationException a) {
            a.printStackTrace();
            return new ResponseEntity<>(null, HttpStatus.UNAUTHORIZED);

        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }


    /**
     *  If the user successfully authenticate the OTP then unity will display a change password page. That will then
     *  make a call to this function.
     * @param data
     */
    @PostMapping("resetPassword/")
    public ResponseEntity<Users> resetOTP(@RequestBody String data) {
        try {
            JSONParser parser = new JSONParser();
            JSONObject json = (JSONObject) parser.parse(data);

            String email = (String) json.get("email");
            String password = (String) json.get("password");

            // This is not sent by the user, rather it is saved from the last call (validateOTP) and sent implicitly.
            // This is done to make sure that one cannot directly reset the password without first having a valid OTP.
            String OTP = (String) json.get("OTP");

            // Once again we must validate the OTP (implicit this time however).
            Users user = otpHandler.validateOTP(email, OTP);

            if (user != null) {
                user.setPassword(encoder.encode(password));
                // Make sure the OTP is now wiped so it cannot be used again.
                user.setOtp("-1");
                userRepository.save(user);
                return new ResponseEntity<>(user, HttpStatus.OK);
            }
            return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);

        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }


}
