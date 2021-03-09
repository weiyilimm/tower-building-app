package com.example.towerbuilderspring.repository;

import com.example.towerbuilderspring.model.Role;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface RoleRepository extends JpaRepository<Role, UUID> {
}
