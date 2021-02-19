package com.example.towerbuilderspring.model;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.IdClass;
import java.io.Serializable;
import java.util.UUID;

@Entity
@IdClass(FriendId.class)
public class Friend implements Serializable {

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
