package com.example.towerbuilderspring.repository;

import com.example.towerbuilderspring.model.Users;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserRepository extends JpaRepository<Users, Long> {
}