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

    @Autowired
    FriendRepository friendRepository;

    @Autowired
    UserRepository userRepository;

    @GetMapping("Users/{id}/Friends")
    public ResponseEntity<List<Users>> getFriends(@PathVariable("id") UUID id) {
        List<Friend> friends = friendRepository.findByUserId(id);
        List<Users> users = new ArrayList<>();

        for (Friend friend : friends) {
            Optional<Users> new_friend = userRepository.findById(friend.getFriendId());
            if (new_friend.isPresent()) {
                users.add(new_friend.get());
            }
        }

        System.out.println(users);

        if (!friends.isEmpty()) {
            return new ResponseEntity<>(users, HttpStatus.OK);
        }
        return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
    }

    @PostMapping("Users/{userId}/Friends/{friendId}")
    public ResponseEntity<Friend> makeFriends(@PathVariable("userId") UUID userId,
                                              @PathVariable("friendId") UUID friendId) {

        Optional<Users> user = userRepository.findById(userId);
        Optional<Users> friend = userRepository.findById(friendId);

        // Check that both parties are existing users.
        if (user.isPresent() && friend.isPresent()) {
            Friend newFriend = new Friend();
            newFriend.setUserId(userId);
            newFriend.setFriendId(friendId);
            friendRepository.save(newFriend);
            return new ResponseEntity<>(newFriend, HttpStatus.OK);
        }
        return new ResponseEntity<>(null, HttpStatus.NO_CONTENT);
    }

    @DeleteMapping("Users/{userId}/Friends/{friendId}")
    public ResponseEntity deleteFriends(@PathVariable("userId") UUID userId,
                                        @PathVariable("friendId") UUID friendId) {

        Optional<Users> user = userRepository.findById(userId);
        Optional<Users> friend = userRepository.findById(friendId);

        if (user.isPresent() && friend.isPresent()) {
            Friend newFriend = new Friend();
            newFriend.setUserId(userId);
            newFriend.setFriendId(friendId);
            friendRepository.delete(newFriend);
            return new ResponseEntity<>(newFriend, HttpStatus.OK);
        }
        return new ResponseEntity<>(null, HttpStatus.NO_CONTENT);
        }
        
}
