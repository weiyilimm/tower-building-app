package com.example.towerbuilderspring.repository;

import com.example.towerbuilderspring.model.WallTextures;
import org.springframework.data.jpa.repository.JpaRepository;

public interface TextureRepository extends JpaRepository<WallTextures, Long> {
}
