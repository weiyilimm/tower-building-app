package com.example.towerbuilderspring.model;


import org.springframework.validation.annotation.Validated;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

@Entity
@Validated
public class BuildingModels {

    @Id
    @NotNull
    private long buildingCode;

    @NotNull
    @Column(unique = true)
    private String buildingName;

    @NotNull
    private int building_xp;

    // Height only exists for the custom building.
    @NotNull
    private int height;

    @NotNull
    private String primaryColour;

    @NotNull
    private String secondaryColour;

    @NotNull
    private long modelGroup; // Each building has it's own "group". When a building is swapped,
                             // the building it swaps with must coincide with it's own group.


    @ManyToMany(mappedBy = "userBuildings", fetch = FetchType.LAZY)
    private Set<Users> users = new HashSet<>();

    public BuildingModels() {};

    public BuildingModels(long buildingCode, String buildingName, long modelGroup, int building_xp, String primaryColour, String secondaryColour) {
        this.buildingCode = buildingCode;
        this.buildingName = buildingName;
        this.modelGroup = modelGroup;
        this.building_xp = building_xp;
        this.primaryColour = primaryColour;
        this.secondaryColour = secondaryColour;
    }

    public int getHeight() {
        return height;
    }

    public void setHeight(int height) {
        this.height = height;
    }

    public String getPrimaryColour() {
        return primaryColour;
    }

    public void setPrimaryColour(String primaryColour) {
        this.primaryColour = primaryColour;
    }

    public String getSecondaryColour() {
        return secondaryColour;
    }

    public void setSecondaryColour(String secondaryColour) {
        this.secondaryColour = secondaryColour;
    }

    public int getBuilding_xp() {
        return building_xp;
    }

    public void setBuilding_xp(int building_xp) {
        this.building_xp = building_xp;
    }

    public long getBuildingCode() {
        return buildingCode;
    }

    public void setBuildingCode(long buildingCode) {
        this.buildingCode = buildingCode;
    }

    public String getBuildingName() {
        return buildingName;
    }

    public void setBuildingName(String buildingName) {
        this.buildingName = buildingName;
    }

    public long getModelGroup() {
        return modelGroup;
    }

    public void setModelGroup(long modelGroup) {
        this.modelGroup = modelGroup;
    }

    public Set<Users> getUsers() {
        return users;
    }

    public void setUsers(Set<Users> users) {
        this.users = users;
    }

    @Override
    public String toString() {
        return "BuildingModels{" +
                "buildingCode=" + buildingCode +
                ", buildingName='" + buildingName + '\'' +
                ", modelGroup=" + modelGroup +
                ", users=" + users +
                '}';
    }
}
