package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.Friend;
import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.repository.FriendRepository;
import com.example.towerbuilderspring.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.UUID;

@RestController
@RequestMapping("/api")
public class FriendController {

    // Get access to the required tables.
    @Autowired
    FriendRepository friendRepository;

    @Autowired
    UserRepository userRepository;

    /**
     * Get all the friends the user has.
     */
    @GetMapping("Users/{id}/Friends")
    public ResponseEntity<List<Users>> getFriends(@PathVariable("id") UUID id) {
        try {
            // Get all the ids of the user's friends.
            List<Friend> friends = friendRepository.findByUserId(id);
            List<Users> users = new ArrayList<>();

            for (Friend friend : friends) {
                // From each friends id, get the actual user detail and store it in a list.
                Optional<Users> new_friend = userRepository.findById(friend.getFriendId());
                // A check to make sure the requested user truly exists.
                if (new_friend.isPresent()) {
                    users.add(new_friend.get());
                }
            }

            // If the user had friends, return the list else return null with a HTTP NOT_FOUND message.
            if (!friends.isEmpty()) {
                return new ResponseEntity<>(users, HttpStatus.OK);
            }
            return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);

        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);

        }
    }

    /**
     *  Register a user as a friend.
     */
    @PostMapping("Users/{userId}/Friends/{friendId}")
    public ResponseEntity<Friend> makeFriends(@PathVariable("userId") UUID userId,
                                              @PathVariable("friendId") UUID friendId) {

        // Get the user and the requested friend from the Users table.
        Optional<Users> user = userRepository.findById(userId);
        Optional<Users> friend = userRepository.findById(friendId);

        // Check that both parties are existing users.
        if (user.isPresent() && friend.isPresent()) {
            // Create a new instance of a Friend.
            Friend newFriend = new Friend();
            newFriend.setUserId(userId);
            newFriend.setFriendId(friendId);
            friendRepository.save(newFriend);
            return new ResponseEntity<>(newFriend, HttpStatus.OK);
        }
        return new ResponseEntity<>(null, HttpStatus.NO_CONTENT);
    }

    /**
     *    Unfriend a user.
     *
     *    (Follows the same idea as makeFriends but removes instead of creates)
     */
    @DeleteMapping("Users/{userId}/Friends/{friendId}")
    public ResponseEntity deleteFriends(@PathVariable("userId") UUID userId,
                                        @PathVariable("friendId") UUID friendId) {

        Optional<Users> user = userRepository.findById(userId);
        Optional<Users> friend = userRepository.findById(friendId);

        if (user.isPresent() && friend.isPresent()) {
            // To delete the friend we need to create a new instance to match against the existing one to delete in the database.
            Friend newFriend = new Friend();
            newFriend.setUserId(userId);
            newFriend.setFriendId(friendId);
            friendRepository.delete(newFriend);
            return new ResponseEntity<>(newFriend, HttpStatus.OK);
        }
        return new ResponseEntity<>(null, HttpStatus.NO_CONTENT);
        }
        
}
