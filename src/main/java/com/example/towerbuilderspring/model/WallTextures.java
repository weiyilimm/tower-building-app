package com.example.towerbuilderspring.model;

import org.springframework.validation.annotation.Validated;

import javax.persistence.*;
import javax.validation.constraints.NotNull;

@Entity
@Validated
@Table(name = "WallTextures")
public class WallTextures {

    @Id
    @Column(name = "wallCode")
    private int wallCode;

    @NotNull
    @Column(name = "wallType", unique = true)
    private String wallType;


    public WallTextures() {};

    public WallTextures(int wallCode, String wallType) {
        this.wallCode = wallCode;
        this.wallType = wallType;
    }

    public int getWallCode() {
        return wallCode;
    }

    public void setWallCode(int wallCode) {
        this.wallCode = wallCode;
    }

    public String getWallType() {
        return wallType;
    }

    public void setWallType(String wallType) {
        this.wallType = wallType;
    }

    @Override
    public String toString() {
        return "WallTextures{" +
                "wallCode=" + wallCode +
                ", wallType='" + wallType + '\'' +
                '}';
    }
}
