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
    private int buildingModel;

    /**
     *      Height is an attribute that only appears for the main building.
     */

    @NotNull
    private int building_xp;

    @NotNull
    private int height;

    @NotNull
    private String primary;

    @NotNull
    private String secondary;

    @NotNull
    private long modelGroup; // Each building has it's own "group". When a building is swapped,
                             // the building it swaps with must coincide with it's own group.




    @ManyToMany(mappedBy = "userBuildings", fetch = FetchType.LAZY)
    private Set<Users> users = new HashSet<>();

    public BuildingModels() {};

    public BuildingModels(long buildingCode, String buildingName, long modelGroup, int building_xp, String primary, String secondary) {
        this.buildingCode = buildingCode;
        this.buildingName = buildingName;
        this.modelGroup = modelGroup;
        this.building_xp = building_xp;
        this.primary = primary;
        this.secondary = secondary;
    }

    public String getPrimary() {
        return primary;
    }

    public void setPrimary(String primary) {
        this.primary = primary;
    }

    public String getSecondary() {
        return secondary;
    }

    public void setSecondary(String secondary) {
        this.secondary = secondary;
    }

    public int getBuildingModel() {
        return buildingModel;
    }

    public void setBuildingModel(int buildingModel) {
        this.buildingModel = buildingModel;
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
