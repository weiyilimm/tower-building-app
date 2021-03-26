package com.example.towerbuilderspring.model;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.IdClass;
import java.io.Serializable;
import java.util.UUID;

/**
 *      This class directly relates to the FRIENDS table.
 *
 *      The friend table has two columns. Both contain user id's, one of the user and one of the friend (who also must
 *      be a user).
 *
 *      This table is used to make and remove friends and is used heavily in conjunction with the USER table.
 */
@Entity
// Here we have implemented a Composite key using @IdClass (instead of @EmbeddedClass)
@IdClass(FriendId.class)
public class Friend implements Serializable {

    // Hence the apparent two primary keys. This actually is treated as a single composite key in the database.
    @Id
    private UUID userId;

    @Id
    private UUID friendId;

    public Friend() {};

    public Friend(UUID userId, UUID friendId) {
        this.userId = userId;
        this.friendId = friendId;
    }

    @Override
    public String toString() {
        return "Friend{" +
                "userId=" + userId +
                ", friendId=" + friendId +
                '}';
    }

    public UUID getUserId() {
        return userId;
    }

    public void setUserId(UUID userId) {
        this.userId = userId;
    }

    public UUID getFriendId() {
        return friendId;
    }

    public void setFriendId(UUID friendId) {
        this.friendId = friendId;
    }
}
