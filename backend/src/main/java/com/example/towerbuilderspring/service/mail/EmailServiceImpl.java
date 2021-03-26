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

/**
 *
 *      EMAIL HANDLER
 *
 */

@Component
public class EmailServiceImpl {

    // The service that actually has implements the email functionality (imported)
    @Autowired
    private JavaMailSender emailSender;

    public EmailServiceImpl () {};

    public void sendSimpleMessage(String to, String subject, String text) throws MailException {
        SimpleMailMessage message = new SimpleMailMessage();

        // The email account that the email is sent from.
        message.setFrom("pseudolabsrobota@gmail.com");
        // The recipent
        message.setTo(to);

        // The subject line.
        message.setSubject(subject);
        // The body (will contain the OTP).
        message.setText(text);

        System.out.println("Created email " + message.toString());

        emailSender.send(message);
    }

    // In the future if you want to send a MIME message with attachments and images, you can use this function.
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