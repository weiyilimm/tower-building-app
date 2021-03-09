package com.example.towerbuilderspring.model;

import javax.persistence.*;
import java.util.UUID;

@Entity
public class Role {
    @GeneratedValue(strategy = GenerationType.AUTO)
    @Id
    private UUID id;

    private String roleName;

    public Role() {};

    public Role(String roleName) {
        this.roleName = roleName;
    }

    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public String getRoleName() {
        return roleName;
    }

    public void setRoleName(String roleName) {
        this.roleName = roleName;
    }
}
