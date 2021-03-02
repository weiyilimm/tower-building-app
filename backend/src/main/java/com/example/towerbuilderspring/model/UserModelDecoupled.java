package com.example.towerbuilderspring.model;

import javax.persistence.EmbeddedId;
import javax.persistence.Entity;
import javax.validation.constraints.NotNull;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.List;

@Entity
public class UserModelDecoupled  {

    @EmbeddedId
    private UserModelIdDecoupled userModelId;

    @NotNull
    private int building_xp;

    @NotNull
    private int height;

    @NotNull
    private int primaryColour;

    @NotNull
    private int secondaryColour;

    @NotNull
    private long modelGroup;

    public UserModelDecoupled() {};

    public UserModelDecoupled(UserModelIdDecoupled userModelId, int building_xp, int height,int primaryColour, int secondaryColour, int modelGroup) {
        this.userModelId = userModelId;
        this.building_xp = building_xp;
        this.height = height;
        this.primaryColour = primaryColour;
        this.secondaryColour = secondaryColour;
        this.modelGroup = modelGroup;
    }

    @Override
    public String toString() {
        return "UserModels{" +
                "userModelId=" + userModelId.toString() +
                ", building_xp=" + building_xp +
                ", height=" + height +
                ", primaryColour=" + primaryColour +
                ", secondaryColour=" + secondaryColour +
                ", modelGroup=" + modelGroup +
                '}';
    }

    public UserModelIdDecoupled getUserModelId() {
        return userModelId;
    }

    public void setUserModelId(UserModelIdDecoupled userModelId) {
        this.userModelId = userModelId;
    }

    public int getBuilding_xp() {
        return building_xp;
    }

    public void setBuilding_xp(int building_xp) {
        this.building_xp = building_xp;
    }

    public int getHeight() {
        return height;
    }

    public void setHeight(int height) {
        this.height = height;
    }

    public int getPrimaryColour() {
        return primaryColour;
    }

    public void setPrimaryColour(int primaryColour) {
        this.primaryColour = primaryColour;
    }

    public int getSecondaryColour() {
        return secondaryColour;
    }

    public void setSecondaryColour(int secondaryColour) {
        this.secondaryColour = secondaryColour;
    }

    public long getModelGroup() {
        return modelGroup;
    }

    public void setModelGroup(long modelGroup) {
        this.modelGroup = modelGroup;
    }

    public LocalDateTime getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(LocalDateTime createdAt) {
        this.createdAt = createdAt;
    }
}
