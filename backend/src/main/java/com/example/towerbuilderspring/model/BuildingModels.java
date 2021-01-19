package com.example.towerbuilderspring.model;


import org.springframework.validation.annotation.Validated;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import java.util.List;

@Entity
@Validated
@Table(name = "BuildingModels")
public class BuildingModels {

    @Id
    @NotNull
    private long buildingCode;

    @NotNull
    @Column(unique = true)      // The name and model number must be unique.
    private String buildingName;

    @OneToMany(fetch = FetchType.LAZY, mappedBy = "models")
    private List<UserTowers> models;

    public BuildingModels() {};

    public BuildingModels(long buildingCode, String buildingName) {
        this.buildingCode = buildingCode;
        this.buildingName = buildingName;
    }

    public long getBuildingCode() {
        return this.buildingCode;
    }

    public String getBuildingName() {
        return this.buildingName;
    }

    public void setBuildingCode(long buildingCode) {
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
