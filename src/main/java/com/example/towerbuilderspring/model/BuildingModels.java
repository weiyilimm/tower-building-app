package com.example.towerbuilderspring.model;


import org.springframework.validation.annotation.Validated;

import javax.persistence.*;
import javax.validation.constraints.NotNull;

@Entity
@Table
@Validated
public class BuildingModels {

    @Id
    @NotNull
    @Column(name = "buildingCode")
    private int buildingCode;

    @NotNull
    @Column(name = "buildingName", unique = true)      // The name and model number must be unique.
    private String buildingName;

    public BuildingModels() {};

    public BuildingModels(int buildingCode, String buildingName) {
        this.buildingCode = buildingCode;
        this.buildingName = buildingName;
    }

    public int getBuildingCode() {
        return this.buildingCode;
    }

    public String getBuildingName() {
        return this.buildingName;
    }

    public void setBuildingCode(int buildingCode) {
        this.buildingCode = buildingCode;
    }

    public void setBuildingName(String buildingName) {
        this.buildingName = buildingName;
    }


    @Override
    public String toString() {
        return "BuildingModels{" +
                "buildingCode=" + buildingCode +
                ", buildingName='" + buildingName + '\'' +
                '}';
    }


}
