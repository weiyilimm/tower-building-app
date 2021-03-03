//package com.example.towerbuilderspring.model;
//
//
//import org.springframework.validation.annotation.Validated;
//
//import javax.persistence.*;
//import javax.validation.constraints.NotNull;
//import java.util.HashSet;
//import java.util.List;
//import java.util.Set;
//
//@Entity
//@Validated
//public class BuildingModels {
//
//    @Id
//    @NotNull
//    private long buildingCode;
//
//    @NotNull
//    @Column(unique = true)
//    private String buildingName;
//
//    @NotNull
//    private long model;
//
//    public BuildingModels() {};
//
//    public BuildingModels(long buildingCode, String buildingName, long modelGroup) {
//        this.buildingCode = buildingCode;
//        this.buildingName = buildingName;
//        this.model = modelGroup;
//    }
//
//    public long getBuildingCode() {
//        return buildingCode;
//    }
//
//    public void setBuildingCode(long buildingCode) {
//        this.buildingCode = buildingCode;
//    }
//
//    public String getBuildingName() {
//        return buildingName;
//    }
//
//    public void setBuildingName(String buildingName) {
//        this.buildingName = buildingName;
//    }
//
//    public long getModelGroup() {
//        return model;
//    }
//
//    public void setModelGroup(long modelGroup) {
//        this.model = modelGroup;
//    }
//
//    @Override
//    public String toString() {
//        return "BuildingModels{" +
//                "buildingCode=" + buildingCode +
//                ", buildingName='" + buildingName + '\'' +
//                ", model=" + model +
//                '}';
//    }
//}
