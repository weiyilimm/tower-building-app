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
    private int towerId;

    @NotNull
    @Column(name = "TowerName")
    private String name;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "UserId")
    private Users user;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "BuildingId")
    private BuildingModels models;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "WindowType")
    private PaneColours colours;

    public UserTowers() {};

    public UserTowers(int towerId, @NotNull String name, Users user, BuildingModels models, PaneColours colours) {
        this.towerId = towerId;
        this.name = name;
        this.user = user;
        this.models = models;
        this.colours = colours;
    }

    public Users getUser() {
        return user;
    }

    public void setUser(Users user) {
        this.user = user;
    }

    public BuildingModels getModels() {
        return models;
    }

    public void setModels(BuildingModels models) {
        this.models = models;
    }

    public PaneColours getColours() {
        return colours;
    }

    public void setColours(PaneColours colours) {
        this.colours = colours;
    }

    public int getTowerId() {
        return towerId;
    }

    public void setTowerId(int towerId) {
        this.towerId = towerId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}
