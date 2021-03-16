package com.example.towerbuilderspring.service.mail;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mail.MailException;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;

@Component
public class EmailServiceImpl {

    @Autowired
    private JavaMailSender emailSender;

    public EmailServiceImpl () {};

    public void sendSimpleMessage(String to, String subject, String text) throws MailException {
        SimpleMailMessage message = new SimpleMailMessage();

        message.setFrom("pseudolabsrobota@gmail.com");
        message.setTo(to);

        message.    setSubject(subject);
        message.setText(text);

        System.out.println("Created email " + message.toString());

        emailSender.send(message);
    }



}