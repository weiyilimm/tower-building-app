package com.example.towerbuilderspring.service.mail;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.FileSystemResource;
import org.springframework.mail.MailException;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;

import javax.mail.MessagingException;
import javax.mail.internet.MimeMessage;
import java.io.File;

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

    public void sendMessageWithAttachment(String to,
                                          String from,
                                          String subject,
                                          String pathToAttachment) throws MessagingException {

        MimeMessage message = emailSender.createMimeMessage();
        MimeMessageHelper helper = new MimeMessageHelper(message, true);

        helper.setFrom(from);
        helper.setTo(to);
        helper.setSubject(subject);

        FileSystemResource file = new FileSystemResource(new File(pathToAttachment));
        helper.addAttachment("Invoice", file);

        emailSender.send(message);
    }


}