//package com.example.towerbuilderspring.controller;
//
//
//import com.example.towerbuilderspring.model.BuildingModels;
//import com.example.towerbuilderspring.repository.ModelRepository;
//import org.springframework.beans.factory.annotation.Autowired;
//import org.springframework.http.HttpStatus;
//import org.springframework.http.ResponseEntity;
//import org.springframework.web.bind.annotation.*;
//
//import java.util.ArrayList;
//import java.util.List;
//import java.util.Optional;
//
//@RestController()
//@RequestMapping("/api")
//public class ModelController {
//
//    @Autowired
//    ModelRepository modelRepository;
//
//    @GetMapping("/Models/")
//    public ResponseEntity<List<BuildingModels>> getAllModels() {
//        try {
//            List<BuildingModels> models = new ArrayList<BuildingModels>();
//            modelRepository.findAll().forEach(models::add);
//
//            if (!models.isEmpty()) {
//                return new ResponseEntity<>(models, HttpStatus.OK);
//            } else {
//                return new ResponseEntity<>(null, HttpStatus.NO_CONTENT);
//            }
//        }
//        catch (Exception e) {
//            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }
//
//    @GetMapping("/Models/{id}")
//    public ResponseEntity<BuildingModels> getModel(@PathVariable long id) {
//        try {
//            BuildingModels model = modelRepository.findById(id).get();
//            return new ResponseEntity<>(model, HttpStatus.OK);
//        } catch (Exception e) {
//            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
//        }
//    }
//
//
//    @PostMapping("/Models")
//    public ResponseEntity<BuildingModels> createModel(@RequestBody BuildingModels model) {
//        try {
//            BuildingModels newModel = new BuildingModels(model.getBuildingCode(), model.getBuildingName(),
//                    model.getModelGroup());
//            modelRepository.save(newModel);
//            return new ResponseEntity<>(newModel, HttpStatus.OK);
//        }
//        catch (Exception e) {
//            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
//        }
//    }
//
//    @PutMapping("/Models/{id}")
//    public ResponseEntity<BuildingModels> updateUser(@PathVariable("id") long id, @RequestBody BuildingModels model) {
//        Optional<BuildingModels> modelData = modelRepository.findById(id);
//
//        if (!modelData.isEmpty()) {
//            BuildingModels modelUpdate = modelData.get();
//            modelUpdate.setBuildingCode(model.getBuildingCode());
//            modelUpdate.setBuildingName(model.getBuildingName());
//            modelUpdate.setModelGroup(model.getModelGroup());
//
//            return new ResponseEntity<>(modelRepository.save(modelUpdate), HttpStatus.OK);
//        } else {
//            return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
//        }
//    }
//
//    @DeleteMapping("/Models/{id}")
//    public ResponseEntity<BuildingModels> deleteUser(@PathVariable("id") long id) {
//        try {
//            modelRepository.deleteById(id);
//            return new ResponseEntity<>(HttpStatus.ACCEPTED);
//        }
//        catch (Exception e) {
//            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
//        }
//    }
//}
