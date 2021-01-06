package com.example.towerbuilderspring;

import com.example.towerbuilderspring.controller.ModelController;
import com.example.towerbuilderspring.model.BuildingModels;
import com.example.towerbuilderspring.repository.ModelRepository;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import static org.assertj.core.internal.bytebuddy.matcher.ElementMatchers.is;
import static org.hamcrest.MatcherAssert.assertThat;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.mockito.AdditionalAnswers.returnsFirstArg;
import static org.mockito.ArgumentMatchers.*;
import static org.mockito.Mockito.when;

@ExtendWith({MockitoExtension.class})
public class TowerBuildingSpringUnitTests {

    // Tests for Building Models
    @Mock
    private ModelRepository modelRepository;

    @InjectMocks
    private ModelController modelController;

    @Test
    void getAllBuildingModels() {
        List<BuildingModels> mockedBuildingsFull = Arrays.asList(new BuildingModels(10, "Prince"),
                new BuildingModels(12, "Boyd Orr"));
        List<BuildingModels> mockedBuildingsEmpty = new ArrayList<>();
        List<BuildingModels> mockedBuildingsError = null;

        when(modelRepository.findAll()).thenReturn(mockedBuildingsFull).thenReturn(mockedBuildingsEmpty).thenReturn(null);

        // Test when models present.
        assertEquals(new ResponseEntity<>(mockedBuildingsFull, HttpStatus.OK), modelController.getAllModels());
        // Test when models absent
        assertEquals(new ResponseEntity<>(null, HttpStatus.NO_CONTENT), modelController.getAllModels());
        // In case of null (or any other error causing value) returned
        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR), modelController.getAllModels());
    }

    @Test
    void getBuildingModelById() {
        BuildingModels mockBuilding = new BuildingModels(1, "Dummy");
        when(modelRepository.findById(1L)).thenReturn(java.util.Optional.of(mockBuilding));
        // If found
        assertEquals(new ResponseEntity<>(mockBuilding, HttpStatus.OK), modelController.getModel(1L));
        // If not found
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), modelController.getModel(2L));
    }

    @Test
    void putBuildingModelInRepository() {
        BuildingModels mockedBuildingToSave = new BuildingModels(1, "Dummy");
        // Return the building model that was just "saved".
        when(modelRepository.save(any(BuildingModels.class))).then(returnsFirstArg());
        assertEquals(new ResponseEntity<>(mockedBuildingToSave, HttpStatus.OK), modelController.createModel(mockedBuildingToSave));
    }

    // Currently think I'll require an application database to test delete functionality.
}
