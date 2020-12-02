package com.example.towerbuilderspring.model;

import org.springframework.validation.annotation.Validated;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import java.util.List;

@Entity
@Validated
@Table(name = "WallTextures")
public class WallTextures {

    @Id
    @Column(name = "wallCode")
    private long wallCode;

    @NotNull
    @Column(name = "wallType", unique = true)
    private String wallType;

    @OneToMany(fetch = FetchType.LAZY, mappedBy = "textures")
    private List<UserTowers> textures;

    public WallTextures() {};

    public WallTextures(long wallCode, String wallType) {
        this.wallCode = wallCode;
        this.wallType = wallType;
    }

    public long getWallCode() {
        return wallCode;
    }

    public void setWallCode(long wallCode) {
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
