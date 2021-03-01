package com.example.towerbuilderspring.repository;

import com.example.towerbuilderspring.model.UserModelDecoupled;
import com.example.towerbuilderspring.model.UserModelIdDecoupled;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserModelRepositoryDecoupled extends JpaRepository<UserModelDecoupled, UserModelIdDecoupled> {
}
