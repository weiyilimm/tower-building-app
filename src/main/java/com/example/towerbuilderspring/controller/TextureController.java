package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.WallTextures;
import com.example.towerbuilderspring.repository.TextureRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("/api")
public class TextureController {

    @Autowired
    TextureRepository textureRepository;

    @GetMapping("/Textures/")
    public ResponseEntity<List<WallTextures>> getAllTextures() {
        try {
            List<WallTextures> textures = new ArrayList<WallTextures>();
            textureRepository.findAll().forEach(textures::add);

            if (!textures.isEmpty()) {
                return new ResponseEntity<>(textures, HttpStatus.OK);
            }
            else {
                return new ResponseEntity<>(null, HttpStatus.NO_CONTENT);
            }
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @GetMapping("/Textures/{id}")
    public ResponseEntity<WallTextures> getTextures(@PathVariable long id) {
        try {
            WallTextures texture = textureRepository.findById(id).get();
            return new ResponseEntity<>(texture, HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @PostMapping("/Textures/")
    public ResponseEntity<WallTextures> createTexture(@RequestBody WallTextures texture) {
        try {
            WallTextures newTexture = new WallTextures(texture.getWallCode(), texture.getWallType());
            textureRepository.save(newTexture);
            return new ResponseEntity<>(newTexture, HttpStatus.OK);
        }
        catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @PutMapping("/Textures/{id}")
    public ResponseEntity<WallTextures> updateTexture(@PathVariable long id, @RequestBody WallTextures texture) {
        Optional<WallTextures> textureData = textureRepository.findById(id);
        if (textureData.isPresent()) {
            WallTextures changeTexture = textureData.get();
            changeTexture.setWallCode(texture.getWallCode());
            changeTexture.setWallType(texture.getWallType());

            return new ResponseEntity<>(textureRepository.save(changeTexture), HttpStatus.ACCEPTED);
        }
        else {
            return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
        }
    }

    @DeleteMapping("/Textures/{id}")
    public ResponseEntity<WallTextures> deleteTexture(@PathVariable long id) {
        try {
            WallTextures deletedTexture = textureRepository.findById(id).get();
            textureRepository.deleteById(id);
            return new ResponseEntity<>(deletedTexture, HttpStatus.ACCEPTED);
        }
        catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

}
