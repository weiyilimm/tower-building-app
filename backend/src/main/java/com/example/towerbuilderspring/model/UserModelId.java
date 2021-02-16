package com.example.towerbuilderspring.model;

import javax.persistence.Embeddable;
import javax.persistence.ManyToOne;
import javax.persistence.MapsId;
import java.io.Serializable;
import java.util.Objects;
import java.util.UUID;

@Embeddable
public class UserModelId implements Serializable {
    private static final long serialVersionUID = 1l;

    @ManyToOne
    @MapsId
    private Users fk_user;

    @ManyToOne
    @MapsId("buildingCode")
    private BuildingModels fk;

    public UserModelId() {};

}
