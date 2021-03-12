package com.example.towerbuilderspring.model;
import org.springframework.validation.annotation.Validated;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import java.time.LocalDateTime;
import java.util.UUID;


@Entity
@Validated
public class Users {
    @Id
    private UUID id;

    @Column(unique = true)
    private String userName;

    @Column(unique = true)
    private String email;    // The email address can be null

    @NotNull
    private String password;

    private int totalexp;        // Default Values

    private LocalDateTime createdAt = LocalDateTime.now();

    public Users() {};

    public Users(String userName, String email, String password, int totalexp) {
        this.id = UUID.randomUUID();
        this.userName = userName;
        this.email = email;
        this.password = password;
        this.totalexp = totalexp;
    }

    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public int getTotalExp() {
        return totalexp;
    }

    public void setTotalExp(int totalexp) {
        this.totalexp = totalexp;
    }

    @Override
    public String toString() {
        return "Users{" +
                "id=" + id +
                ", userName='" + userName + '\'' +
                ", email='" + email + '\'' +
                ", password='" + password + '\'' +
                ", totalexp=" + totalexp +
                '}';
    }
}
