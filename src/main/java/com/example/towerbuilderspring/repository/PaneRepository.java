package com.example.towerbuilderspring.repository;

import com.example.towerbuilderspring.model.PaneColours;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PaneRepository extends JpaRepository<PaneColours, Long> {

}
