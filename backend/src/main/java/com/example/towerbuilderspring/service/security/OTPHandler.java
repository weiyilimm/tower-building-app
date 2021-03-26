package com.example.towerbuilderspring.service.security;

import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.repository.UserRepository;
import javassist.NotFoundException;
import org.aspectj.weaver.ast.Not;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Component;

import javax.naming.AuthenticationException;

@Component
public class OTPHandler {

    @Autowired
    UserRepository userRepository;

    private PasswordEncoder encoder = new BCryptPasswordEncoder();

    public String generateOTP()
    {   //int randomPin declared to store the otp
        //since we using Math.random() hence we have to type cast it int
        //because Math.random() returns decimal value
        int randomValue  =(int) (Math.random()*9000)+1000;

        String otp = String.valueOf(randomValue);

        return otp; //returning value of otp
    }

    public void saveOTP(String email, String OTP) throws NotFoundException {
        Users user = userRepository.findByEmail(email);
        if (user != null) {
            // Encode the OTP to make sure it cannot be used.
            String encodedOTP = encoder.encode(OTP);
            user.setOtp(encodedOTP);
            userRepository.save(user);
        }
        else {
            throw new NotFoundException("The user is not found");
        }
    }

    public Users validateOTP(String email, String OTP) throws NotFoundException, AuthenticationException {
        Users user = userRepository.findByEmail(email);

        // This is the default value of the OTP
        if (OTP.equals("-1")) {
            throw new AuthenticationException("Reset Password Request not made");
        }

        // If the encrypted input and the (already encrypted) value from the database match.
        if (user != null && encoder.matches(OTP, user.getOtp())) {
            System.out.println(user.getOtp());
            System.out.println(OTP);
            // return the user.
            return user;
        } else if (user != null) {
            throw new NotFoundException("Password was incorrect");
        } else {
            throw new NotFoundException("The user was not found");
        }
    }
}
