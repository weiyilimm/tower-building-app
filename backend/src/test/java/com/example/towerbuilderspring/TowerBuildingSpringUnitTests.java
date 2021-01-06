package com.example.towerbuilderspring;

import com.example.towerbuilderspring.controller.ModelController;
import com.example.towerbuilderspring.controller.PaneController;
import com.example.towerbuilderspring.model.BuildingModels;
import com.example.towerbuilderspring.model.PaneColours;
import com.example.towerbuilderspring.repository.ModelRepository;
import com.example.towerbuilderspring.repository.PaneRepository;
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

    // Tests for Building Controller.
    @Mock
    private ModelRepository modelRepository;

    @InjectMocks
    private ModelController modelController;

    @Test
    public void getBuildingModels() {
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
    public void getBuildingModelById() {
        BuildingModels mockBuilding = new BuildingModels(1, "Dummy");
        when(modelRepository.findById(1L)).thenReturn(java.util.Optional.of(mockBuilding));
        // If found
        assertEquals(new ResponseEntity<>(mockBuilding, HttpStatus.OK), modelController.getModel(1L));
        // If not found
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), modelController.getModel(2L));
    }

    @Test
    public void createBuildingModelInRepository() {
        BuildingModels mockedBuildingToSave = new BuildingModels(1, "Dummy");
        // Return the building model that was just "saved".
        when(modelRepository.save(any(BuildingModels.class))).then(returnsFirstArg());
        // Todo - Find another way to perfom check
        assertEquals(new ResponseEntity<>(mockedBuildingToSave, HttpStatus.OK), modelController.createModel(mockedBuildingToSave));
    }

    @Test
    public void updateBuildingModelInRepository()
    {
        BuildingModels mockOriginalBuilding = new BuildingModels(1, "DummyOriginal");
        BuildingModels mockUpdatedBuilding = new BuildingModels(1, "Dummy Updated");

        when(modelRepository.findById(1l)).thenReturn(java.util.Optional.of(mockOriginalBuilding));
        when(modelRepository.save(any(BuildingModels.class))).thenReturn(mockUpdatedBuilding);

        // The user exists.
        assertEquals(new ResponseEntity<>(mockUpdatedBuilding, HttpStatus.OK),
                modelController.updateUser(1l, new BuildingModels(1, "Dummy Updated")));
        // The user does not exist.
        assertEquals(new ResponseEntity<>(null, HttpStatus.NOT_FOUND), modelController.updateUser(2l,
                new BuildingModels(1, "Dummy Updated")));

    }



    // Currently think I'll require an application database to test delete functionality (due to need of actual database
    // to delete from).


    // Test for PaneController.
    @Mock
    PaneRepository paneRepository;

    @InjectMocks
    PaneController paneController;

    @Test
    public void getAllPanes() {
        List<PaneColours> mockPanesFull = Arrays.asList(new PaneColours(1, "Red"),
                new PaneColours(2, "Blue"), new PaneColours(3, "Green"));
        List<PaneColours> mockPanesEmpty = new ArrayList<>();

        // Valid, valid but empty, invalid output tests respectively.
        when(paneRepository.findAll()).thenReturn(mockPanesFull).thenReturn(mockPanesEmpty).thenReturn(null);

        assertEquals(new ResponseEntity<>(mockPanesFull, HttpStatus.OK), paneController.getAllWindows());
        assertEquals(new ResponseEntity<>(null, HttpStatus.NO_CONTENT), paneController.getAllWindows());
        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR), paneController.getAllWindows());
    }

    @Test
    public void getPaneById() {
        PaneColours mockPaneColour = new PaneColours(1, "Blue");
        when(paneRepository.findById(1l)).thenReturn(java.util.Optional.of(mockPaneColour));

        assertEquals(new ResponseEntity<>(mockPaneColour, HttpStatus.OK), paneController.getWindow(1l));
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), paneController.getWindow(2l));
    }

    // Todo learn how to force an exception in mockinto.

    @Test
    public void createPaneInRepository() {
        PaneColours mockPaneColour = new PaneColours(1, "Blue");
        PaneColours badMock = new PaneColours(666, "Crimison");
        when(paneRepository.save(any(PaneColours.class))).then(returnsFirstArg());
        //when(paneRepository.save(badMock)).thenReturn();

        assertEquals(new ResponseEntity<>(paneRepository.save(mockPaneColour), HttpStatus.OK), paneController.createWindow(mockPaneColour));
        //assertEquals(new ResponseEntity<>(HttpStatus.BAD_REQUEST), paneController.createWindow(badMock));
    }

    @Test
    public void updatePaneInRepository()
    {
        PaneColours mockedOriginalPane = new PaneColours(1, "blue");
        PaneColours mockedUpdatedPane = new PaneColours(1, "green");

        when(paneRepository.findById(1l)).thenReturn(java.util.Optional.of(mockedOriginalPane));
        when(paneRepository.save(any(PaneColours.class))).thenReturn(mockedUpdatedPane);

        // Pane colour updated succesfully.
        assertEquals(new ResponseEntity<>(mockedUpdatedPane, HttpStatus.OK),
                paneController.updateWindow(1, new PaneColours(1, "green")));
        // Requested colour to replace doesn't exist.
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND),
                paneController.updateWindow(2, new PaneColours(2, "green")));
    }



}
