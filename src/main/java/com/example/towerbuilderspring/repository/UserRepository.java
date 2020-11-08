package com.example.towerbuilderspring.repository;


import com.example.towerbuilderspring.model.User;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface UserRepository extends JpaRepository<User, Long >{
    // Custom finder methods in addition to all the find methods provided by JPA framework.
    List<User> findByValid(boolean valid);
    List<User> findByDescription(String description);
}


