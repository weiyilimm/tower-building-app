package com.example.towerbuilderspring.model;
import org.springframework.validation.annotation.Validated;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;
import java.util.UUID;


@Entity
@Validated
public class Users {
    @Id
    private UUID id;

    @Column(unique = true)
    private String userName;

    @Column(unique = true)
    private String email;    // The email address can be null

    @NotNull
    private String password;

    private int totalexp = 0;        // Default Values

    @ManyToMany(fetch = FetchType.LAZY, cascade = CascadeType.PERSIST)
    @JoinTable(name = "user_towers",
            joinColumns = {
                @JoinColumn(name = "user_id", referencedColumnName = "id",
                        nullable = false, updatable = false)},
            inverseJoinColumns = {
                @JoinColumn(name = "building_id", referencedColumnName = "buildingCode",
                        nullable = false, updatable = false)})

    private Set<BuildingModels> userBuildings = new HashSet<>();

    public Users() {};

    public Users(String userName, String email, String password, int totalexp) {
        this.id = UUID.randomUUID();
        this.userName = userName;
        this.email = email;
        this.password = password;
        this.totalexp = totalexp;
    }

    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public int getTotalExp() {
        return totalexp;
    }

    public void setTotalExp(int totalexp) {
        this.totalexp = totalexp;
    }

    public Set<BuildingModels> getUserBuildings() {
        return userBuildings;
    }

    public BuildingModels getUserBuilding(BuildingModels buildingModel) {
        if (userBuildings.contains(buildingModel)) {
            return buildingModel;
        }
        else{
            return null;
        }
    }

    public BuildingModels deleteUserBuilding(long id) {
        for (Iterator<BuildingModels> it = this.userBuildings.iterator(); it.hasNext();) {
            BuildingModels buildingByGroup = it.next();
            if (buildingByGroup.getBuildingCode() == id) {
                this.userBuildings.remove(buildingByGroup);
                return buildingByGroup;
            }
        }
        return null;
    }

    public BuildingModels findByBuildingGroup(long group) {
        for (Iterator<BuildingModels> it = this.userBuildings.iterator(); it.hasNext();) {
            BuildingModels buildingByGroup = it.next();
            if (buildingByGroup.getModelGroup() == group ){
                return buildingByGroup;
            }
        }
        return null;
    }

    public void addUserBuilding(BuildingModels building) {
        this.userBuildings.add(building);
    }

    @Override
    public String toString() {
        return "Users{" +
                "userName='" + userName + '\'' +
                ", email='" + email + '\'' +
                ", password='" + password + '\'' +
                ", totalxp=" + totalexp +
                ", userBuildings=" + userBuildings +
                '}';
    }
}
