package com.example.towerbuilderspring.service.security;


import org.springframework.stereotype.Component;

@Component
public class GenerateOTP {
    public String generateOTP()
    {   //int randomPin declared to store the otp
        //since we using Math.random() hence we have to type cast it int
        //because Math.random() returns decimal value
        int randomPin   =(int) (Math.random()*9000)+1000;
        String otp  = String.valueOf(randomPin);
        return otp; //returning value of otp
    }
//    public static void main(String args[])//method to call and print otp
//    {
//        String otpSting  =generateOTP();//function calling
//        System.out.println("OTP : "+otpSting);
//    }
}

