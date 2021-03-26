package com.example.towerbuilderspring.model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

/**
 *
 *      This table doesn't yet performing anything in the app yet. However it allows one to creates roles in the
 *      application. So further on, if you want to allow only certain users to perform some tasks (such as
 *      deleting a user directly using the app) then you can set the role and permission in this table.
 *
 */

@Entity
public class Role {
    @GeneratedValue(strategy = GenerationType.AUTO)
    @Id
    private long id;

    private String roleName;

    public Role() {};

    public Role(String roleName) {
        this.roleName = roleName;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getRoleName() {
        return roleName;
    }

    public void setRoleName(String roleName) {
        this.roleName = roleName;
    }
}
