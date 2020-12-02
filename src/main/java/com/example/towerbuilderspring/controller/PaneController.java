package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.PaneColours;
import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.repository.PaneRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.swing.text.html.HTML;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("/api")
public class PaneController {

    @Autowired
    PaneRepository paneRepository;

    @GetMapping("/Panes/")
    public ResponseEntity<List<PaneColours>> getAllWindows() {

        try {
            List<PaneColours> windows = new ArrayList<>();
            paneRepository.findAll().forEach(windows::add);

            if (!windows.isEmpty()) {
                return new ResponseEntity<>(windows, HttpStatus.OK);
            } else {
                return new ResponseEntity<>(null, HttpStatus.NO_CONTENT);
            }
        }
        catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @GetMapping("/Panes/{id}")
    public ResponseEntity<PaneColours> getWindow(@PathVariable long id) {
        try {
            PaneColours pane = paneRepository.findById(id).get();
            return new ResponseEntity<>(pane, HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }


    @PostMapping("/Panes/")
    public ResponseEntity<PaneColours> createWindow(@RequestBody PaneColours window) {
        try {
            PaneColours newWindow = new PaneColours(window.getColourCode(), window.getColours());
            return new ResponseEntity<>(paneRepository.save(newWindow), HttpStatus.OK);
        }
        catch (Exception e)
        {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @PutMapping("/Panes/{id}")
    public ResponseEntity<PaneColours> updateWindow(@PathVariable long id, @RequestBody PaneColours window) {
        try {
            Optional<PaneColours> windowData = paneRepository.findById(id);

            if (!windowData.isEmpty()) {
                PaneColours changeWindow = windowData.get();
                changeWindow.setColourCode(window.getColourCode());
                changeWindow.setColours(window.getColours());

                return new ResponseEntity<>(paneRepository.save(changeWindow), HttpStatus.OK);
            }
            else {
                return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
            }
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @DeleteMapping("/Panes/{id}")
    public ResponseEntity<PaneColours> deleteWindow(@PathVariable long id) {
        try {
            PaneColours deleted = paneRepository.findById(id).get();
            paneRepository.deleteById(id);
            return new ResponseEntity<>(deleted, HttpStatus.ACCEPTED);
        }
        catch(Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}
