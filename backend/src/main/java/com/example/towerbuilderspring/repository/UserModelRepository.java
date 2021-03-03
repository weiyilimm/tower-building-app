package com.example.towerbuilderspring.repository;

import com.example.towerbuilderspring.model.UserModelId;
import com.example.towerbuilderspring.model.UserModels;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserModelRepository extends JpaRepository<UserModels, UserModelId> {
    UserModels findByUserModelIdAndAndModelGroup(UserModelId id, long group);
}
