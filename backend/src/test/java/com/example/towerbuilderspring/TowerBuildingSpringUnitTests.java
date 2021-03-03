package com.example.towerbuilderspring;

import com.example.towerbuilderspring.controller.*;
//import com.example.towerbuilderspring.model.BuildingModels;
import com.example.towerbuilderspring.model.Users;

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
import static org.mockito.Mockito.when;

@ExtendWith({MockitoExtension.class})
public class TowerBuildingSpringUnitTests {

    /***
     *
     *      WORKING TEST.
     *
     */
    @Mock
    private UserRepository mockUserRepository;


    // This tells the controller class to use the dummy repository in this setting.
    @InjectMocks
    private UserController mockUserController;


    /***
     * Notes by Vedant:
     *
     * The Tests are to test if the endpoints are acting as expecting, that is, if the controller is working
     * correctly. We do not need to test if the repository functions (such as userRepository.findalL()) are working
     * as they are built in.
     *
     * Currently, for the unit tests, we test if we can reach the expected endpoint scenarios (usually OK, NOT_FOUND
     * or INTERNAL_SERVER_ERROR) depending on the request.
     */
    @Test
    public void testIfGetsAllUsers() {

        /***
         * The process of mocking of a repository is as follows.
         *  1. Create dummy value(s).
         *  2. Create a dummy method to replace the know working repository method. This is the hardcoding the
         *  return value of the method when it is called.
         *  3. Checking if the return value from the controller matches the dummy value to be returned.
         */
        List<Users> mockedUsersPresent = Arrays.asList(new Users("Henry", "henry@email.com", "Scrafty", 10),
                new Users("Barry", "Barry@email.com", "Hoops", 21));
        List<Users> mockedUsersAbsent = new ArrayList<>();

        // The Repository is "mocked". In here I've used a shortcut the will call the next return statement every time it's called.
        when(mockUserRepository.findAll()).thenReturn(mockedUsersPresent).thenReturn(mockedUsersAbsent).thenReturn(null);

        // Run the tests for each of the different possible scenarios.
        assertEquals(new ResponseEntity<>(mockedUsersPresent, HttpStatus.OK), mockUserController.getAllUsers());
        assertEquals(new ResponseEntity<>(HttpStatus.NO_CONTENT), mockUserController.getAllUsers());
        assertEquals(new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR), mockUserController.getAllUsers());
    }
}
//
//
//    // Tests for Building Controller.
//    @Mock
//    private ModelRepository modelRepository;
//
//    @InjectMocks
//    private ModelController modelController;
//
//    @Test
//    public void getBuildingModels() {
//        List<BuildingModels> mockedBuildingsFull = Arrays.asList(new BuildingModels(10, "Prince"),
//                new BuildingModels(12, "Boyd Orr"));
//        List<BuildingModels> mockedBuildingsEmpty = new ArrayList<>();
//        List<BuildingModels> mockedBuildingsError = null;
//
//        when(modelRepository.findAll()).thenReturn(mockedBuildingsFull).thenReturn(mockedBuildingsEmpty).thenReturn(null);
//
//        // Test when models present.
//        assertEquals(new ResponseEntity<>(mockedBuildingsFull, HttpStatus.OK), modelController.getAllModels());
//        // Test when models absent
//        assertEquals(new ResponseEntity<>(null, HttpStatus.NO_CONTENT), modelController.getAllModels());
//        // In case of null (or any other error causing value) returned
//        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR), modelController.getAllModels());
//    }
//
//    @Test
//    public void getBuildingModelById() {
//        BuildingModels mockBuilding = new BuildingModels(1, "Dummy");
//        when(modelRepository.findById(1L)).thenReturn(java.util.Optional.of(mockBuilding));
//        // If found
//        assertEquals(new ResponseEntity<>(mockBuilding, HttpStatus.OK), modelController.getModel(1L));
//        // If not found
//        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), modelController.getModel(2L));
//    }
//
//    @Test
//    public void createBuildingModelInRepository() {
//        BuildingModels mockedBuildingToSave = new BuildingModels(1, "Dummy");
//        // Return the building model that was just "saved".
//        when(modelRepository.save(any(BuildingModels.class))).then(returnsFirstArg());
//        // Todo - Find another way to perfom check
//        assertEquals(new ResponseEntity<>(mockedBuildingToSave, HttpStatus.OK), modelController.createModel(mockedBuildingToSave));
//    }
//
//    @Test
//    public void updateBuildingModelInRepository()
//    {
//        BuildingModels mockOriginalBuilding = new BuildingModels(1, "DummyOriginal");
//        BuildingModels mockUpdatedBuilding = new BuildingModels(1, "Dummy Updated");
//
//        when(modelRepository.findById(1l)).thenReturn(java.util.Optional.of(mockOriginalBuilding));
//        when(modelRepository.save(any(BuildingModels.class))).thenReturn(mockUpdatedBuilding);
//
//        // The user exists.
//        assertEquals(new ResponseEntity<>(mockUpdatedBuilding, HttpStatus.OK),
//                modelController.updateUser(1l, new BuildingModels(1, "Dummy Updated")));
//        // The user does not exist.
//        assertEquals(new ResponseEntity<>(null, HttpStatus.NOT_FOUND), modelController.updateUser(2l,
//                new BuildingModels(1, "Dummy Updated")));
//
//    }
//
//
//
//    // Currently think I'll require an application database to test delete functionality (due to need of actual database
//    // to delete from).
//
//
//    // Tests for User Controller
//    @Mock
//    UserRepository userRepository;
//
//    @InjectMocks
//    UserController userController;
//
//
//    @Test
//    public void getUserModels() {
//        List<Users> mockedUsersFull = Arrays.asList(new Users(UUID.randomUUID(), "CameronLit2", "test@mail.com", "password", 0, 12),
//                new Users(UUID.randomUUID(), "ReubanYo", "test2@mail.com", "password", 0, 32));
//        List<Users> mockedUsersEmpty = new ArrayList<>();
//        List<Users> mockedBuildingsError = null;
//
//        when(userRepository.findAll()).thenReturn(mockedUsersFull).thenReturn(mockedUsersEmpty).thenReturn(null);
//
//        // Test when models present.
//        assertEquals(new ResponseEntity<>(mockedUsersFull, HttpStatus.OK), userController.getAllUsers());
//        // Test when models absent
//        assertEquals(new ResponseEntity<>(null, HttpStatus.NO_CONTENT), userController.getAllUsers());
//        // In case of null (or any other error causing value) returned
//        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR), userController.getAllUsers());
//    }
//
//    @Test
//    public void getUserModelById() {
//        Users mockBuilding = new Users("CameronLit2", "test@mail.com", "password", 0, 12);
//        when(userRepository.findById(1L)).thenReturn(java.util.Optional.of(mockBuilding));
//        // If found
//        assertEquals(new ResponseEntity<>(mockBuilding, HttpStatus.OK), userController.getUser(1L));
//        // If not found
//        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), userController.getUser(2L));
//    }
//
//    @Test
//    public void createUserModelInRepository() {
//        Users mockedBuildingToSave = new Users("Wei23","test@mail.com", "password", 0, 12);
//        // Return the building model that was just "saved".
//        when(userRepository.save(any(Users.class))).then(returnsFirstArg());
//        // Todo - Find another way to perform check
//        assertEquals(new ResponseEntity<>(mockedBuildingToSave, HttpStatus.OK), userController.createUser(mockedBuildingToSave));
//    }
//
//    // Updating user is slightly different as I get an id from the database, need to look into that.
//
////    @Test
////    public void updateBuildingModelInRepository()
////    {
////        Users mockOriginalBuilding = new Users("test@mail.com", "password", 0, 12);
////        Users mockUpdatedBuilding = new Users("test@mail.com", "password", 0, 14);
////
////        when(userRepository.findById(1l)).thenReturn(java.util.Optional.of(mockOriginalBuilding));
////        when(userRepository.save(any(Users.class))).thenReturn(mockUpdatedBuilding);
////
////        // The user exists.
////        assertEquals(new ResponseEntity<>(mockUpdatedBuilding, HttpStatus.OK),
////                userController.updateUser(1l, new Users(1, "Dummy Updated")));
////        // The user does not exist.
////        assertEquals(new ResponseEntity<>(null, HttpStatus.NOT_FOUND), userController.updateUser(2l,
////                new Users(1, "Dummy Updated")));
////
////    }
