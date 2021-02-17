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

    public UserModelId(Users user, BuildingModels model) {
        this.fk_user = user;
        this.fk = model;
    }

    @Override
    public String toString() {
        return "UserModelId{" +
                "fk_user=" + fk_user +
                ", fk=" + fk +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof UserModelId)) return false;
        UserModelId that = (UserModelId) o;
        return fk_user.equals(that.fk_user) &&
                Objects.equals(fk, that.fk);
    }

    public static long getSerialVersionUID() {
        return serialVersionUID;

    }@Override
    public int hashCode() {
        return Objects.hash(fk_user, fk);
    }

    public Users getFk_user() {
        return fk_user;
    }

    public void setFk_user(Users fk_user) {
        this.fk_user = fk_user;
    }

    public BuildingModels getFk_building() {
        return fk;
    }

    public void setFk_building(BuildingModels fk) {
        this.fk = fk;
    }
}
