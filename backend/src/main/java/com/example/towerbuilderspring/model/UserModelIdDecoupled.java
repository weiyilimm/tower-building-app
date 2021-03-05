package com.example.towerbuilderspring.model;

import javax.persistence.Embeddable;
import javax.persistence.ManyToOne;
import javax.persistence.MapsId;
import java.io.Serializable;
import java.util.Objects;

@Embeddable
public class UserModelIdDecoupled implements Serializable {
    private static final long serialVersionUID = 1l;

    @ManyToOne
    @MapsId
    private Users fk_user;

    private long model;

    public UserModelIdDecoupled() {};

    public UserModelIdDecoupled(Users user, long model) {
        this.fk_user = user;
        this.model = model;
    }

    @Override
    public String toString() {
        return "UserModelId{" +
                "fk_user=" + fk_user +
                ", model=" + model +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof UserModelIdDecoupled)) return false;
        UserModelIdDecoupled that = (UserModelIdDecoupled) o;
        return model == that.model &&
                Objects.equals(fk_user, that.fk_user);
    }

    public static long getSerialVersionUID() {
        return serialVersionUID;

    }@Override
    public int hashCode() {
        return Objects.hash(fk_user, model);
    }

    public Users getFk_user() {
        return fk_user;
    }

    public void setFk_user(Users fk_user) {
        this.fk_user = fk_user;
    }

    public long getModel() {
        return model;
    }

    public void setModel(long model) {
        this.model = model;
    }
}
