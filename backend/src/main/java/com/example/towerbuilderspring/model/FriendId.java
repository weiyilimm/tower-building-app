package com.example.towerbuilderspring.model;

import java.io.Serializable;
import java.util.Objects;
import java.util.UUID;

public class FriendId implements Serializable {

    private UUID userId;
    private UUID friendId;

    public FriendId() {};

    public FriendId(UUID userId, UUID friendId) {
        this.userId = userId;
        this.friendId = friendId;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof FriendId)) return false;
        FriendId friendId1 = (FriendId) o;
        return userId.equals(friendId1.userId) &&
                friendId.equals(friendId1.friendId);
    }

    @Override
    public int hashCode() {
        return Objects.hash(userId, friendId);
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
