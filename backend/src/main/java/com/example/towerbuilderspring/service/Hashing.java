package com.example.towerbuilderspring.service;

import com.example.towerbuilderspring.model.BuildingModels;
import org.springframework.context.annotation.Bean;


import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class Hashing {
    public Hashing() {};

    public String md5(String salt, String plainText) throws NoSuchAlgorithmException {

        MessageDigest md = MessageDigest.getInstance("MD5");

        if (salt != null) {
            md.update(salt.getBytes());
        }
        md.update(plainText.getBytes());

        byte byteData[] = md.digest();



        StringBuffer sb = new StringBuffer();
        for (int i = 0; i < byteData.length; i++) {
            sb.append(Integer.toString((byteData[i] & 0xff) + 0x100, 16).substring(1));
        }
        return sb.toString();
    }


}
