package com.example.towerbuilderspring;

import com.example.towerbuilderspring.controller.*;
import com.example.towerbuilderspring.model.BuildingModels;
import com.example.towerbuilderspring.model.PaneColours;
import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.model.WallTextures;
import com.example.towerbuilderspring.repository.ModelRepository;
import com.example.towerbuilderspring.repository.PaneRepository;
import com.example.towerbuilderspring.repository.TextureRepository;
import com.example.towerbuilderspring.repository.UserRepository;
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

        // Pane colour updated successfully.
        assertEquals(new ResponseEntity<>(mockedUpdatedPane, HttpStatus.OK),
                paneController.updateWindow(1, new PaneColours(1, "green")));
        // Requested colour to replace doesn't exist.
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND),
                paneController.updateWindow(2, new PaneColours(2, "green")));
    }


    // Tests for the textures

    @Mock
    TextureRepository textureRepository;

    @InjectMocks
    TextureController textureController;

    @Test
    public void getAllTextures() {
        List<WallTextures> mockedTexturesFull = Arrays.asList(new WallTextures(1, "Brick"),
                new WallTextures(2, "Stone"), new WallTextures(3, "Marble"));
        List<WallTextures> mockedTexturesEmpty = new ArrayList<>();

        when(textureRepository.findAll()).thenReturn(mockedTexturesFull).thenReturn(mockedTexturesEmpty).thenReturn(null);

        // Test when models present.
        assertEquals(new ResponseEntity<>(mockedTexturesFull, HttpStatus.OK), textureController.getAllTextures());
        // Test when models absent
        assertEquals(new ResponseEntity<>(null, HttpStatus.NO_CONTENT), textureController.getAllTextures());
        // In case of null (or any other error causing value) returned
        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR), textureController.getAllTextures());
    }
    
    @Test
    public void getTextureById() {
        WallTextures mockedWallTexture = new WallTextures(1, "Brick");
        when(textureRepository.findById(1l)).thenReturn(java.util.Optional.of(mockedWallTexture));
        
        assertEquals(new ResponseEntity<>(mockedWallTexture, HttpStatus.OK), textureController.getTextures(1l));
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), textureController.getTextures(2l));
    }



    @Test
    public void createTextureInRepository() {
        WallTextures mockWallTexture = new WallTextures(1, "Brick");
        WallTextures badMock = new WallTextures(666, "Dirt");
        when(paneRepository.save(any(PaneColours.class))).then(returnsFirstArg());
        //when(paneRepository.save(badMock)).thenReturn();

        assertEquals(new ResponseEntity<>(textureRepository.save(mockWallTexture), HttpStatus.OK), textureController.createTexture(mockWallTexture));
        //assertEquals(new ResponseEntity<>(HttpStatus.BAD_REQUEST), paneController.createWindow(badMock));
    }

    @Test
    public void updateTextureInRepository()
    {
        WallTextures mockOriginalTexture = new WallTextures(1, "Brick");
        WallTextures mockUpdatedTexture = new WallTextures(1, "Steel");

        when(textureRepository.findById(1l)).thenReturn(java.util.Optional.of(mockOriginalTexture));
        when(textureRepository.save(any(WallTextures.class))).thenReturn(mockUpdatedTexture);

        // Pane colour updated successfully.
        assertEquals(new ResponseEntity<>(mockUpdatedTexture, HttpStatus.OK),
                textureController.updateTexture(1, new WallTextures(1, "Steel")));
        // Requested colour to replace doesn't exist.
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND),
                textureController.updateTexture(2, new WallTextures(2, "Sand")));
    }
    
    // Tests for User Controller

    @Mock
    UserRepository userRepository;

    @InjectMocks
    UserController userController;


    @Test
    public void getUserModels() {
        List<Users> mockedUsersFull = Arrays.asList(new Users("test@mail.com", "password", 0, 12),
                new Users("test2@mail.com", "password", 0, 32));
        List<Users> mockedUsersEmpty = new ArrayList<>();
        List<Users> mockedBuildingsError = null;

        when(userRepository.findAll()).thenReturn(mockedUsersFull).thenReturn(mockedUsersEmpty).thenReturn(null);

        // Test when models present.
        assertEquals(new ResponseEntity<>(mockedUsersFull, HttpStatus.OK), userController.getAllUsers());
        // Test when models absent
        assertEquals(new ResponseEntity<>(null, HttpStatus.NO_CONTENT), userController.getAllUsers());
        // In case of null (or any other error causing value) returned
        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR), userController.getAllUsers());
    }

    @Test
    public void getUserModelById() {
        Users mockBuilding = new Users("test@mail.com", "password", 0, 12);
        when(userRepository.findById(1L)).thenReturn(java.util.Optional.of(mockBuilding));
        // If found
        assertEquals(new ResponseEntity<>(mockBuilding, HttpStatus.OK), userController.getUser(1L));
        // If not found
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), userController.getUser(2L));
    }

    @Test
    public void createUserModelInRepository() {
        Users mockedBuildingToSave = new Users("test@mail.com", "password", 0, 12);
        // Return the building model that was just "saved".
        when(userRepository.save(any(Users.class))).then(returnsFirstArg());
        // Todo - Find another way to perform check
        assertEquals(new ResponseEntity<>(mockedBuildingToSave, HttpStatus.OK), userController.createUser(mockedBuildingToSave));
    }

    // Updating user is slightly different as I get an id from the database, need to look into that.

//    @Test
//    public void updateBuildingModelInRepository()
//    {
//        Users mockOriginalBuilding = new Users("test@mail.com", "password", 0, 12);
//        Users mockUpdatedBuilding = new Users("test@mail.com", "password", 0, 14);
//
//        when(userRepository.findById(1l)).thenReturn(java.util.Optional.of(mockOriginalBuilding));
//        when(userRepository.save(any(Users.class))).thenReturn(mockUpdatedBuilding);
//
//        // The user exists.
//        assertEquals(new ResponseEntity<>(mockUpdatedBuilding, HttpStatus.OK),
//                userController.updateUser(1l, new Users(1, "Dummy Updated")));
//        // The user does not exist.
//        assertEquals(new ResponseEntity<>(null, HttpStatus.NOT_FOUND), userController.updateUser(2l,
//                new Users(1, "Dummy Updated")));
//
//    }

}
