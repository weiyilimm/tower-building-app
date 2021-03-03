package com.example.towerbuilderspring.repository;

import com.example.towerbuilderspring.model.Friend;
import com.example.towerbuilderspring.model.FriendId;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.UUID;

public interface FriendRepository extends JpaRepository<Friend, FriendId> {
    List<Friend> findByUserId(UUID user);
}
