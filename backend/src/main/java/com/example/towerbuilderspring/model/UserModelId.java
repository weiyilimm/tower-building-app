package com.example.towerbuilderspring.model;

import javax.persistence.Embeddable;
import javax.persistence.ManyToOne;
import javax.persistence.MapsId;
import java.io.Serializable;
import java.util.Objects;
import java.util.UUID;


/**
 *      Here we create a composite Primary key. The @Embeddable annotation allows us to use this as a primary
 *      key in USER_MODELS.
 *
 *      This allows to always have a unique id which also links the user table making sure that for a row to be
 *      added to the USER_MODELS table, the respective user must exist in the USERS table first.
 */

@Embeddable
public class UserModelId implements Serializable {
    private static final long serialVersionUID = 1l;

    // A user can have many models.
    @ManyToOne
    // This will map to the Primary key column of the user table (id).
    @MapsId
    private Users fk_user;

    private long model;

    public UserModelId() {};

    public UserModelId(Users user, long model) {
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
        if (!(o instanceof UserModelId)) return false;
        UserModelId modelId = (UserModelId) o;
        return model == modelId.model &&
                Objects.equals(fk_user, modelId.fk_user);
    }

    @Override
    public int hashCode() {
        return Objects.hash(fk_user, model);
    }

    public static long getSerialVersionUID() {
        return serialVersionUID;

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


