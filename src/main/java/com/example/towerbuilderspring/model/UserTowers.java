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
    @Column(name = "TowerName")
    private String name;

//    @ManyToOne(fetch = FetchType.LAZY)
//    @JoinColumn(name = "UserId")
//    private Users user;

    @Column(name = "UserId")
    private long user;

    @Column(name = "BuildingName")
    private long models;

    @Column(name = "WindowType")
    private long colours;

    @Column(name = "WallTexture")
    private long textures;

//    @ManyToOne(fetch = FetchType.LAZY)
//    @JoinColumn(name = "BuildingId")
//    private BuildingModels models;
//
//    @ManyToOne(fetch = FetchType.LAZY)
//    @JoinColumn(name = "WindowType")
//    private PaneColours colours;
//
//    @ManyToOne(fetch = FetchType.LAZY)
//    @JoinColumn(name = "WallType")
//    private WallTextures textures;

    public UserTowers() {};

//    public UserTowers(@NotNull String name, Users user, BuildingModels models, PaneColours colours, WallTextures textures) {
//        this.name = name;
//        this.user = user;
//        this.models = models;
//        this.colours = colours;
//        this.textures = textures;
//    }

    public UserTowers(@NotNull String name, long user, long models, long colours, long textures) {
        this.name = name;
        this.user = user;
        this.models = models;
        this.colours = colours;
        this.textures = textures;
    }


    public long getUser(){
        return user;
    }

    public void setUser(long user) {
        this.user = user;
    }

    public long getTowerId() {
        return towerId;
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

    public long getColours() {
        return colours;
    }

    public void setColours(long colours) {
        this.colours = colours;
    }

    public long getTextures() {
        return textures;
    }

    public void setTextures(long textures) {
        this.textures = textures;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }


    //    public Users getUser() {
//        return user;
//    }
//
//    public void setUser(Users user) {
//        this.user = user;
//    }


//    public BuildingModels getModels() {
//        return models;
//    }
//
//    public void setModels(BuildingModels models) {
//        this.models = models;
//    }
//
//    public PaneColours getColours() {
//        return colours;
//    }
//
//    public void setColours(PaneColours colours) {
//        this.colours = colours;
//    }
//
//    public WallTextures getTextures() {
//        return textures;
//    }
//
//    public void setTextures(WallTextures textures) {
//        this.textures = textures;
//    }
//
//    public long getTowerId() {
//        return towerId;
//    }
//
//    public void setTowerId(long towerId) {
//        this.towerId = towerId;
//    }
}
