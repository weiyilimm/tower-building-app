package com.example.towerbuilderspring.model;

import javax.persistence.*;

@Entity
@Table(name = "Users")
public class Users {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long userId;

    @Column(name = "email")
    private String email = null;    // The email address can be null

    @Column(name = "password")
    private String password;        // This will have to be salted

    @Column(name = "totalxp")
    private int totalxp = 0;        // Default Values

    @Column(name = "score")
    private int score = 0;


    protected Users() {};

    public Users(String email, String password, int totalxp, int score) {
        this.email = email;
        this.password = password;
        this.totalxp = totalxp;
        this.score = score;
    }

    public long getUserId() {
        return userId;
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
