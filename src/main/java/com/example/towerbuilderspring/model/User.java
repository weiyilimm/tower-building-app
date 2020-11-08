package com.example.towerbuilderspring.model;

import javax.persistence.*;

public class User {

    @Id // This is the primary key of the database
    @GeneratedValue(strategy = GenerationType.AUTO) // Creates a new id for each new user via auto increment.
    private long id;

    @Column(name = "name")
    private String name;

    @Column(name = "towers")
    private String towers;

    @Column(name = "valid")
    private boolean  valid;

    public User() {};

    public User(String name, String towers, boolean valid){
        this.name = name;
        this.towers = towers;
        this.valid = valid;
    }

    public long getId(){
        return id;
    }

    public String getName(){
        return name;
    }

    public String getTowers(){
        return towers;
    }

    public boolean getValid(){
        return valid;
    }

    public void setName(String name){
        this.name = name;
    }

    public void setTowers(String towers){
        this.towers= towers;
    }

    public void setValid(boolean  valid){
        this.valid = valid;
    }

    @Override
    public String toString(){
        return "Username: " + name + " Has towers: " + towers + " valid user: " + valid + " ";
    }


}
