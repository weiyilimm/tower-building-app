package com.example.towerbuilderspring.model;

import org.springframework.validation.annotation.Validated;

import javax.persistence.*;
import javax.validation.constraints.NotNull;

@Entity
@Validated
@Table(name = "UserTowers")
public class UserTowers {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long towerId;


    // Columns Unique to User Towers
    @Column(name = "TowerName", unique = true)
    private String towerName;

    @NotNull                // This shouldn't be required nonetheless.
    @Column(name = "xp")
    private int xp = 0;

}
