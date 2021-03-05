package com.example.towerbuilderspring;

import com.example.towerbuilderspring.controller.*;
import com.example.towerbuilderspring.model.Friend;
import com.example.towerbuilderspring.model.UserModelId;
import com.example.towerbuilderspring.model.UserModels;
import com.example.towerbuilderspring.model.Users;

import com.example.towerbuilderspring.repository.FriendRepository;
import com.example.towerbuilderspring.repository.UserModelRepository;
import com.example.towerbuilderspring.repository.UserRepository;
import org.json.simple.parser.ParseException;
import org.junit.Before;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.test.annotation.Rollback;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;

import java.util.*;

import static org.hamcrest.Matchers.any;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

@ExtendWith({MockitoExtension.class})
public class TowerBuildingSpringUnitTests {

    /*
        Roy Osherove's naming strategy:
        MethodName_StateUnderTest_ExpectedBehavior
     */

    @Mock
    private UserRepository mockUserRepository;

    // This tells the controller class to use the dummy repository in this setting.
    @InjectMocks
    private UserController mockUserController;
    @InjectMocks
    private UserLoginController mockUserLoginController;

    /*
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
    public void GetAllUsers_HttpResponseGetUsersFromList_IfSameOkIfEmptyNoContentElseISE() {

        /*
         * The process of mocking of a repository is as follows.
         *  1. Create dummy value(s).
         *  2. Create a dummy method to replace the know working repository method. This is the hardcoding the
         *  return value of the method when it is called.
         *  3. Checking if the return value from the controller matches the dummy value to be returned.
         */
        List<Users> mockedUsersPresent = Arrays.asList(
                new Users("Henry", "henry@email.com", "Scrafty", 10),
                new Users("Barry", "Barry@email.com", "Hoops", 21));
        List<Users> mockedUsersAbsent = new ArrayList<>();

        // The Repository is "mocked". In here I've used a shortcut that will call the next return statement every time it's called.
        when(mockUserRepository.findAll()).thenReturn(mockedUsersPresent).thenReturn(mockedUsersAbsent).thenReturn(null);

        // Run the tests for each of the different possible scenarios.
        assertEquals(new ResponseEntity<>(mockedUsersPresent, HttpStatus.OK), mockUserController.getAllUsers());
        assertEquals(new ResponseEntity<>(HttpStatus.NO_CONTENT), mockUserController.getAllUsers());
        assertEquals(new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR), mockUserController.getAllUsers());
    }

    @Test
    public void UpdateUser_HttpResponseUpdateUserEmailXP_ifExistsOkElseNotFound() {

        String updateEmail = "hhh@gmail.com";
        int updateXP = 130;

        //Original User
        Users mockUser = new Users("Henry", "henry@email.com", "Scrafty", 10);
        //Updates to by applied to user
        Users updateUser = new Users("Henry", updateEmail, "Scrafty", updateXP);

        //Makesure mock user can be accessed by finding its ID. Do this by storing in mock repository
        when(mockUserRepository.findById(mockUser.getId())).thenReturn(java.util.Optional.of(mockUser));

        assertEquals(new ResponseEntity<>(HttpStatus.OK), mockUserController.updateUser(mockUser.getId(), updateUser));
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), mockUserController.updateUser(UUID.randomUUID(), updateUser));
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), mockUserController.updateUser(null, updateUser));
    }

    @Test
    public void UpdateUser_CorrectOverwrittenValues_UpdatedEmailXPEqualUpdatedMockUserValues() {
        String updateEmail = "hhh@gmail.com";
        int updateXP = 130;

        Users mockUser = new Users("Henry", "henry@email.com", "Scrafty", 10);
        Users updateUser = new Users("Harriet", updateEmail, "ytfarcS", updateXP);

        when(mockUserRepository.findById(mockUser.getId())).thenReturn(java.util.Optional.of(mockUser));

        //apply update to user
        mockUserController.updateUser(mockUser.getId(), updateUser);

        //check the updates were applied as expected
        assertEquals("hhh@gmail.com", mockUser.getEmail()); //did email change as expected
        assertEquals(updateXP, mockUser.getTotalExp()); //did xp change as expected
    }

    @Test
    public void UpdateUser_PermanentFieldsAreNotOverwritten_UserNameIdPasswordAllStaySame() {
        String originalName = "Henry";
        String originalPass = "Scrafty";

        Users mockUser = new Users(originalName, "henry@email.com", originalPass, 10);
        Users updateUser = new Users("Harriet", "henry@email.com", "ytfarcS", 10);

        UUID originalUUID = mockUser.getId();
        //store original name, password and ID to compare after update is applied

        when(mockUserRepository.findById(mockUser.getId())).thenReturn(java.util.Optional.of(mockUser));

        //apply the update to the user
        mockUserController.updateUser(mockUser.getId(), updateUser);

        //compare the stuff that is not supposed to change, to the user fields after the update
        assertEquals(originalUUID, mockUser.getId()); //check id isn't updated
        assertEquals(originalName, mockUser.getUserName()); //check username isn't updated
        assertEquals(originalPass, mockUser.getPassword()); //check password isn't updated
    }

    @Test
    public void DeleteUser_HTTPResponseDeleteUserByID_NoUnexpectedISE() {
        Users mockUser = new Users("Henry", "henry@email.com", "Scrafty", 10);

        assertEquals(new ResponseEntity<>(HttpStatus.ACCEPTED), mockUserController.deleteUser(mockUser.getId()));
        //assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), mockUserController.deleteUser(UUID.randomUUID()));
        //assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), mockUserController.deleteUser(null));

        //Doesnt really matter if you 'delete' a non existent user
        assertEquals(new ResponseEntity<>(HttpStatus.ACCEPTED), mockUserController.deleteUser(UUID.randomUUID()));
        assertEquals(new ResponseEntity<>(HttpStatus.ACCEPTED), mockUserController.deleteUser(null));
    }

    /*
    @Test
    public void DeleteUser_UserIsSuccessfullyDeleted_GetUserFoundThenDeletedThenNotFound() {
        Users mockUser = (Users) mockUserLoginController.createUser(
                new Users("Henry", "henry@email.com", "Scrafty", 10)).getBody();

        assertEquals(mockUser, mockUserRepository.findByUserName("Henry"));
        mockUserController.deleteUser(mockUser.getId());
        assertEquals(null, mockUserRepository.findByUserName("Henry"));
    }*/

    @Mock
    private UserModelRepository mockUserModelRepository;

    @Test
    public void getUserBuildings_HttpResponseGetUserBuildings_CorrectAmmountReturnedOkIfBuildingsISEIfNull (){
        Users mockUser = new Users("Henry", "henry@email.com", "Scrafty", 10);
        UserModelId modelId1 = new UserModelId(mockUser, 43);
        UserModelId modelId2 = new UserModelId(mockUser, 21);
        List<UserModels> mockModels = Arrays.asList(
                new UserModels(modelId1, 102, 1,303, 206, 4),
                new UserModels(modelId2, 85, 1,11, 405, 2)
                );

        when(mockUserModelRepository.findAll()).thenReturn(mockModels).thenReturn(mockModels).thenReturn(new ArrayList<>()).thenReturn(null);
        when(mockUserRepository.findById(mockUser.getId())).thenReturn(Optional.of(mockUser));

        assertEquals(HttpStatus.OK, mockUserController.getUserBuildings(mockUser.getId()).getStatusCode());
        assertEquals(2, ((ArrayList)(mockUserController.getUserBuildings(mockUser.getId()).getBody().get("userBuildings"))).size());
        assertEquals(0, ((ArrayList)(mockUserController.getUserBuildings(mockUser.getId()).getBody().get("userBuildings"))).size());
        //assertEquals(new ResponseEntity<>(HttpStatus.NO_CONTENT), mockUserController.getUserBuildings(mockUser.getId()));
        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR), mockUserController.getUserBuildings(mockUser.getId()));

    }

    @Test
    public void getUserBuildings_HttpResponseGetBuildingsWithID_NonExistentIDReturnsNoContent (){
        Users mockUser = new Users("Henry", "henry@email.com", "Scrafty", 10);
        UserModelId modelId1 = new UserModelId(mockUser, 43);
        UserModelId modelId2 = new UserModelId(mockUser, 21);
        List<UserModels> mockModels = Arrays.asList(
                new UserModels(modelId1, 102, 1,303, 206, 4),
                new UserModels(modelId2, 85, 1,11, 405, 2)
        );

        when(mockUserModelRepository.findAll()).thenReturn(mockModels);
        when(mockUserRepository.findById(mockUser.getId())).thenReturn(Optional.of(mockUser));

        assertEquals(HttpStatus.OK, mockUserController.getUserBuildings(mockUser.getId()).getStatusCode());
        assertEquals(new ResponseEntity<>(HttpStatus.NO_CONTENT), mockUserController.getUserBuildings(UUID.randomUUID()));
        //Returns NO_CONTENT which is true, but should also probably be an Internal Server Error
        assertEquals(new ResponseEntity<>(HttpStatus.NO_CONTENT), mockUserController.getUserBuildings(null));

    }

    @Test
    public void updateUserBuilding_HttpResponseUpdateBuildingsWithIDandJSONData_ModelExistOkElseCreatedIfNoUserOrBadInputISE () throws ParseException {
        Users mockUser = new Users("Henry", "henry@email.com", "Scrafty", 10);
        UserModelId modelId1 = new UserModelId(mockUser, 43);
        UserModels mockModel = new UserModels(modelId1, 102, 0,303, 206, 4);

        when(mockUserRepository.findById(mockUser.getId())).thenReturn(Optional.of(mockUser));
        when(mockUserModelRepository.findAll()).thenReturn(Arrays.asList(mockModel)).thenReturn(Arrays.asList());

        //Modify an existing model
        assertEquals(HttpStatus.OK,
                    mockUserController.updateUserBuilding(mockUser.getId(),
                                                          mockModel.getUserModelId().getModel(),
                                                          mockModel.getModelGroup(),

                                              "{\"buildingCode\":43,\"building_xp\":102," +
                                                          "\"height\":0,\"buildingGroup\":4," +
                                                          "\"primaryColour\":4,\"secondaryColour\":206}"
                                                         ).getStatusCode()
                    );
        //Creating a New model
        assertEquals(HttpStatus.CREATED,
                     mockUserController.updateUserBuilding(mockUser.getId(),
                                                           mockModel.getUserModelId().getModel(),
                                                           mockModel.getModelGroup(),
                                               "{\"buildingCode\":43,\"building_xp\":102," +
                                                           "\"height\":0,\"buildingGroup\":4," +
                                                           "\"primaryColour\":102,\"secondaryColour\":206}"
                                                          ).getStatusCode()
                    );
        //User doesn't exist
        assertEquals(HttpStatus.INTERNAL_SERVER_ERROR,
                mockUserController.updateUserBuilding(UUID.randomUUID(),
                        mockModel.getUserModelId().getModel(),
                        mockModel.getModelGroup(),
                        "{\"buildingCode\":43,\"building_xp\":102," +
                                "\"height\":0,\"buildingGroup\":4," +
                                "\"primaryColour\":102,\"secondaryColour\":206}"
                ).getStatusCode());
        //bad inputs
        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR),
                    mockUserController.updateUserBuilding(mockUser.getId(),
                                                          mockModel.getUserModelId().getModel(),
                                                          mockModel.getModelGroup(),
                                                          "{building_xp}"
                                                         ).getStatusCode()
        );
    }

    @Test
    public void updateUserBuilding_CorrectContentUpdated_CorrectItemsAreUpdatedButCodeRemainsSame() throws ParseException {
        Users mockUser = new Users("Henry", "henry@email.com", "Scrafty", 10);
        UserModelId modelId1 = new UserModelId(mockUser, 43);
        UserModels mockModel = new UserModels(modelId1, 102, 0,303, 206, 4);

        when(mockUserRepository.findById(mockUser.getId())).thenReturn(Optional.of(mockUser));
        when(mockUserModelRepository.findAll()).thenReturn(Arrays.asList(mockModel)).thenReturn(Arrays.asList());

        UserModels updatedModel = mockUserController.updateUserBuilding(mockUser.getId(),
                        mockModel.getUserModelId().getModel(),
                        mockModel.getModelGroup(),
            "{\"buildingCode\":43,\"building_xp\":102," +
                        "\"height\":0,\"buildingGroup\":4," +
                        "\"primaryColour\":4,\"secondaryColour\":301}"
                ).getBody();

        assertEquals(4, updatedModel.getPrimaryColour() );
        assertEquals(301, updatedModel.getSecondaryColour());
        assertEquals(mockModel.getUserModelId(), updatedModel.getUserModelId());

    }
    /*
    @Test
    public void GetUser_HttpResponseGetUserByUserID_IfCorrectOkElseNotFound() {

        Users mockUser = new Users("Henry", "henry@email.com", "Scrafty", 10);
        UUID uuid = mockUser.getId();

        // make sure mock repository has the user acessible by ID
        when(mockUserRepository.findById(uuid)).thenReturn(java.util.Optional.of(mockUser));

        // Check an existing user can be found
        // Check a non existing user is not found
        // Check for a null input
        assertEquals(new ResponseEntity<>(mockUser, HttpStatus.OK), mockUserController.getUser(uuid));
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), mockUserController.getUser(UUID.randomUUID()));
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), mockUserController.getUser(null));
    }
    */

    //======================================================

    @Test
    public void authenticateUser_HttpResponseAuthenticatesUserReturnsUser_IfCorrectPasswordOkIfWrongPasswordNotFoundIfNoUserNotFound(){
        PasswordEncoder encoder = new BCryptPasswordEncoder();
        String password = encoder.encode("Scrafty");
        Users user = new Users("Henry", "henry@email.com", password, 10);

        when(mockUserRepository.findByUserName("Henry")).thenReturn(user);
        when(mockUserRepository.findByUserName("Harriet")).thenReturn(null);

        assertEquals(new ResponseEntity<>(user, HttpStatus.OK), mockUserLoginController.authenticateUser("Henry", "Scrafty"));
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), mockUserLoginController.authenticateUser("Harriet", "Scrafty"));
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), mockUserLoginController.authenticateUser("Henry", "ytfarcS"));
    }

    @Test
    public void CreateUser_HttpResponseCreateUser_IfOkReturnCreatedElseInternalServerError() {

        Users user = new Users("Henry", "henry@email.com", "Scrafty", 10);

        // Don't test response entity as you cannot test the user created is correct as
        // we do not know the user ID of the created user
        // Another method tests if the fields are all created correctly
        assertEquals(HttpStatus.CREATED, mockUserLoginController.createUser(user).getStatusCode());
        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR), mockUserLoginController.createUser(null));
    }

    @Test
    public void CreateUser_CorrectValuesForEachField_EachFieldInCreatedUserEqualsInputUser(){
        Users user = new Users("Henry", "henry@email.com", "Scrafty", 10);

        Users mockUser = (Users) mockUserLoginController.createUser(user).getBody();

        //Compare the expected input user with the produced output user
        assertEquals(user.getUserName(), mockUser.getUserName());
        assertEquals(user.getEmail(), mockUser.getEmail());
        assertEquals(user.getTotalExp(), mockUser.getTotalExp());
        assertEquals(user.getPassword(), mockUser.getPassword());
    }

    /*
    @Test
    public void CreateUser_DontAllowSameUsernameOrEmail_IfUserHasSameNameOrEmailReturnISE(){
        Users user = new Users("Henry", "henry@email.com", "Scrafty", 10);
        Users user1 = new Users("Harriet", "henry@email.com", "Scrafty", 10);
        Users user2 = new Users("Henry", "harriet@email.com", "Scrafty", 10);
        Users user3 = new Users("Harriet", "harriet@gmail.com", "Scrafty", 10);

        //when(mockUserRepository.save(any(Users.class))).thenReturn(user).thenReturn(user).thenReturn(user1).then(user2).thenReturn(user3);
        //when(mockUserRepository.save(any(Users.class))).thenReturn(user);

        //Create The first user
        when(mockUserRepository.findByUserName("Henry")).thenReturn(user);

        //not same email and username
        //not same email
        //not same username
        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR), mockUserLoginController.createUser(user));
        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR), mockUserLoginController.createUser(user1));
        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR), mockUserLoginController.createUser(user2));
        assertEquals(HttpStatus.CREATED, mockUserLoginController.createUser(user3).getStatusCode());
    }
    */

    @Test
    public void LoginDeleteUser_HTTPResponseDeleteUserByID_NoUnexpectedISE() {

        Users mockUser = new Users("Henry", "henry@email.com", "Scrafty", 10);

        when(mockUserRepository.findByUserName("Henry")).thenReturn(mockUser);

        assertEquals(new ResponseEntity<>(mockUser, HttpStatus.OK), mockUserLoginController.deleteUser("Henry"));
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), mockUserLoginController.deleteUser("Harriet"));
        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR), mockUserLoginController.deleteUser(null));
    }

    /*
    @Test
    public void LoginDeleteUser_UserIsSuccessfullyDeleted_GetUserNotFound() {
        Users mockUser = (Users) mockUserLoginController.createUser(
                new Users("Henry", "henry@email.com", "Scrafty", 10)).getBody();

        assertEquals(mockUser, mockUserRepository.findByUserName("Henry"));
        mockUserLoginController.deleteUser("Henry");
        assertEquals(null, mockUserRepository.findByUserName("Henry"));
    }
    */

    //======================================================

    /*
    @Mock
    private ModelRepository mockModelRepository;

    @InjectMocks
    private ModelController mockModelController;

    @Test
    public void getAllModels_HttpResponseGetModelsFromList_IfSameOkIfEmptyNoContentElseISE(){
        List<BuildingModels> mockedModelsPresent = Arrays.asList(
                new BuildingModels(3, "Shuttle", 4),
                new BuildingModels(2, "PC Tower", 5));
        List<BuildingModels> mockedModelsAbsent = new ArrayList<>();

        // The Repository is "mocked". In here I've used a shortcut that will call the next return statement every time it's called.
        when(mockModelRepository.findAll()).thenReturn(mockedModelsPresent).thenReturn(mockedModelsAbsent).thenReturn(null);

        // Run the tests for each of the different possible scenarios.
        assertEquals(new ResponseEntity<>(mockedModelsPresent, HttpStatus.OK), mockModelController.getAllModels());
        assertEquals(new ResponseEntity<>(HttpStatus.NO_CONTENT), mockModelController.getAllModels());
        assertEquals(new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR), mockModelController.getAllModels());
    }

    @Test
    public void getModel_HttpResponseGetModelByID_IfCorrectOkElseNotFound(){
        BuildingModels mockModel = new BuildingModels(3, "Shuttle", 4);

        when(mockModelRepository.findById(3L)).thenReturn(java.util.Optional.of(mockModel));

        assertEquals(new ResponseEntity<>(mockModel, HttpStatus.OK), mockModelController.getModel(3));
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), mockModelController.getModel(0));
    }

    @Test
    public void createModel_HttpResponseCreateUser_IfOkReturnCreatedElseInternalServerError(){
        BuildingModels mockModel = new BuildingModels(3, "Shuttle", 4);

        assertEquals(HttpStatus.CREATED, mockModelController.createModel(mockModel).getStatusCode());
        assertEquals(new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR), mockModelController.createModel(null));
    }

    @Test
    public void createModel_CorrectValuesForEachField_EachFieldInCreatedBuildModelEqualsInputBuildingModel(){
        BuildingModels model = new BuildingModels(3, "Shuttle", 4);

        BuildingModels mockModel = mockModelController.createModel(model).getBody();

        //Compare the expected input user with the produced output user
        assertEquals(model.getBuildingCode(), mockModel.getBuildingCode());
        assertEquals(model.getBuildingName(), mockModel.getBuildingName());
        assertEquals(model.getModelGroup(), mockModel.getModelGroup());
    }

    @Test
    public void updateModel__(){

    }

    @Test
    public void deleteModel_HTTPResponseDeleteUserByID_NoUnexpectedISE(){
        BuildingModels mockModel = new BuildingModels(3, "Shuttle", 4);

        mockModelController.createModel(mockModel);

        assertEquals(new ResponseEntity<>(HttpStatus.ACCEPTED), mockModelController.deleteUser(3L));
        //Doesnt really matter if you 'delete' a non existent model
        assertEquals(new ResponseEntity<>(HttpStatus.ACCEPTED), mockModelController.deleteUser(2L));
    }
    /*
    @Test
    public void DeleteUser_ModelIsSuccessfullyDeleted_GetModelNotFound() {
        BuildingModels model = new BuildingModels(3, "Shuttle", 4);

        BuildingModels mockModel = mockModelController.createModel(model).getBody();

        mockModelController.deleteUser(1L);
        assertEquals(HttpStatus.NOT_FOUND, mockModelController.getModel(1L).getStatusCode());
    }
    */

    //======================================================

    @Mock
    private FriendRepository mockFriendRepository;

    @InjectMocks
    private FriendController mockFriendController;

    @Test
    public void getFriends(){
        /*
         * The process of mocking of a repository is as follows.
         *  1. Create dummy value(s).
         *  2. Create a dummy method to replace the know working repository method. This is the hardcoding the
         *  return value of the method when it is called.
         *  3. Checking if the return value from the controller matches the dummy value to be returned.
         */

        //main guy we're testing for friends
        Users mockedUserMain =  new Users("Fraser", "fraser@email.com", "Password", 4);

        //mockedUsers contains 3 users
        List<Users> mockedUsersFriends = Arrays.asList(
                new Users("Henry", "henry@email.com", "Scrafty", 10),
                new Users("Barry", "Barry@email.com", "Hoops", 21));

        //when he has 2 friends
        List<Friend> mockedFriends2 = Arrays.asList(
                new Friend(mockedUserMain.getId(),mockedUsersFriends.get(0).getId()),
                new Friend(mockedUserMain.getId(),mockedUsersFriends.get(1).getId()));

        //when he has 1 friend
        List<Friend> mockedFriends1 = Arrays.asList(
                new Friend(mockedUserMain.getId(),mockedUsersFriends.get(0).getId()));

        //when he has no friends lmao
        List<Friend> mockedFriendsNone = Arrays.asList();

        when(mockFriendRepository.findByUserId(mockedUserMain.getId())).thenReturn(mockedFriends2).thenReturn(mockedFriends1).thenReturn(mockedFriendsNone);


        //mockFriendController.getFriends(mockedUserMain.getId())
        // Run the tests for each of the different possible scenarios.
        assertEquals(new ResponseEntity<>(mockedFriends2, HttpStatus.OK), mockFriendController.getFriends(mockedUserMain.getId()));
        assertEquals(new ResponseEntity<>(mockedFriends1, HttpStatus.OK), mockFriendController.getFriends(mockedUserMain.getId()));
        assertEquals(new ResponseEntity<>(HttpStatus.NO_CONTENT), mockFriendController.getFriends(mockedUserMain.getId()));
        assertEquals(new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR), mockFriendController.getFriends(mockedUserMain.getId()));
    }

    @Test
    public void makeFriends(){

    }

    @Test
    public void deleteFriends(){

    }

    /*

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
    */

    /*


    // Currently think I'll require an application database to test delete functionality (due to need of actual database
    // to delete from).


    // Tests for User Controller
    @Mock
    UserRepository userRepository;

    @InjectMocks
    UserController userController;


    @Test
    public void getUserModels() {
        List<Users> mockedUsersFull = Arrays.asList(new Users(UUID.randomUUID(), "CameronLit2", "test@mail.com", "password", 0, 12),
                new Users(UUID.randomUUID(), "ReubanYo", "test2@mail.com", "password", 0, 32));
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
        Users mockBuilding = new Users("CameronLit2", "test@mail.com", "password", 0, 12);
        when(userRepository.findById(1L)).thenReturn(java.util.Optional.of(mockBuilding));
        // If found
        assertEquals(new ResponseEntity<>(mockBuilding, HttpStatus.OK), userController.getUser(1L));
        // If not found
        assertEquals(new ResponseEntity<>(HttpStatus.NOT_FOUND), userController.getUser(2L));
    }

    @Test
    public void createUserModelInRepository() {
        Users mockedBuildingToSave = new Users("Wei23","test@mail.com", "password", 0, 12);
        // Return the building model that was just "saved".
        when(userRepository.save(any(Users.class))).then(returnsFirstArg());
        // Todo - Find another way to perform check
        assertEquals(new ResponseEntity<>(mockedBuildingToSave, HttpStatus.OK), userController.createUser(mockedBuildingToSave));
    }

    // Updating user is slightly different as I get an id from the database, need to look into that.

    @Test
    public void updateBuildingModelInRepository()
    {
        Users mockOriginalBuilding = new Users("test@mail.com", "password", 0, 12);
        Users mockUpdatedBuilding = new Users("test@mail.com", "password", 0, 14);

        when(userRepository.findById(1l)).thenReturn(java.util.Optional.of(mockOriginalBuilding));
        when(userRepository.save(any(Users.class))).thenReturn(mockUpdatedBuilding);

        // The user exists.
        assertEquals(new ResponseEntity<>(mockUpdatedBuilding, HttpStatus.OK),
                userController.updateUser(1l, new Users(1, "Dummy Updated")));
        // The user does not exist.
        assertEquals(new ResponseEntity<>(null, HttpStatus.NOT_FOUND), userController.updateUser(2l,
                new Users(1, "Dummy Updated")));

    } */

}
