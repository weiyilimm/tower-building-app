package com.example.towerbuilderspring.model;

import org.springframework.validation.annotation.Validated;

import javax.persistence.*;
import javax.validation.constraints.NotNull;

@Entity
@Validated
@Table(name = "UserTowers")
public class UserTowers {

    @Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    private long towerId;

    @NotNull
    private String name;

    private long user;

    private long models;

    private short primary = 200;

    private short secondary = 201;

    public UserTowers() {};

    public UserTowers(@NotNull String name, long user, long models, short primary, short secondary) {
        this.name = name;
        this.user = user;
        this.models = models;
        this.primary = primary;
        this.secondary = secondary;
    }


    public long getUser(){
        return user;
    }

    public void setUser(long user) {
        this.user = user;
    }

    public void setTowerId(long towerId) {
        this.towerId = towerId;
    }

    public long getModels() {
        return models;
    }

    public void setModels(long models) {
        this.models = models;
    }

    public short getPrimaryColour() {
        return primary;
    }

    public short getSecondaryColour() {
        return secondary;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }


}
