package com.example.towerbuilderspring.repository;

import com.example.towerbuilderspring.model.UserTowers;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.List;

// One must create a separate repository for each entity

public interface UserTowerRepository extends JpaRepository<UserTowers, Long> {
    // Custom methods to be installed here.
}
