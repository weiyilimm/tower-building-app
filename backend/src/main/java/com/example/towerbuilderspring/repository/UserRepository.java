package com.example.towerbuilderspring.repository;

import com.example.towerbuilderspring.model.Users;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.Optional;

public interface UserRepository extends JpaRepository<Users, Long> {
    List<Users> findByEmail(String email);
}