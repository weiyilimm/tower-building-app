package com.example.towerbuilderspring.repository;

import com.example.towerbuilderspring.model.BuildingModels;
import com.example.towerbuilderspring.model.Users;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ModelRepository extends JpaRepository<BuildingModels, Long> {
}