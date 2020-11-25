package com.example.towerbuilderspring.repository;

import com.example.towerbuilderspring.model.Users;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.List;

public interface UserRepository extends JpaRepository<Users, Long> {

    // Custom methods to be installed here.

}
