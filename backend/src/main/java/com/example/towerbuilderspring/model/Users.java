package com.example.towerbuilderspring.model;

import org.springframework.validation.annotation.Validated;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import java.util.List;


@Entity
@Validated
@Table(name = "Users", uniqueConstraints = {
        @UniqueConstraint(columnNames = "ID")
})
public class Users {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    @Column(name = "ID", unique = true, nullable = false)
    private long userId;

    @Column(name = "EMAIL")
    private String email = null;    // The email address can be null

    @NotNull
    @Column(name = "PASSWORD")
    private String password;

    @NotNull
    @Column(name = "TOTALXP")
    private int totalxp = 0;        // Default Values

    @NotNull
    @Column(name = "SCORE")
    private int score = 0;

    @OneToMany(fetch = FetchType.LAZY, mappedBy = "user")
    private List<UserTowers> users;


    public Users() {};

    public Users(String email, String password, int totalxp, int score) {
        this.email = email;
        this.password = password;
        this.totalxp = totalxp;
        this.score = score;
    }

    public long getUserId() {
        return userId;
    }

    public String getPassword() {
        return password;
    }

    public String getEmail() {
        return email;
    }

    // No method to get the password right now (first need to figure out how to salt)

    public int getTotalXp() {
        return totalxp;
    }

    public int getScore() {
        return score;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public void setTotalXP(int xp) {
        this.totalxp = xp;
    }

    public void setScore(int score) {
        this.score = score;
    }

    @Override
    public String toString() {
        return "User Id: " + userId + " Email: " + email + " totalXP: " + totalxp + " score: " + score;
    };

}
