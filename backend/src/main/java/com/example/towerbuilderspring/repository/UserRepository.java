package com.example.towerbuilderspring.repository;

import com.example.towerbuilderspring.model.Users;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface UserRepository extends JpaRepository<Users, UUID> {
    Users findByUserName(String name);
    Users findByEmail(String email);
}