﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class ChangeClickImage : MonoBehaviour
{
    public Sprite normalImage;
    public Sprite clickedImage;
    public Button icon;
    private bool isRotate = true;

    public CameraMovePhone movement;


    void Start()
    {
        icon = GetComponent<Button>();
    }

    //Rotation Icon
    public void RotationIcon()
    {
        if (isRotate == true){
            icon.image.overrideSprite = clickedImage;
            isRotate = false;
   
        }
        else{
            icon.image.overrideSprite = normalImage;
            isRotate = true;
        }

        //boolean in camera movement is true if panning. this script is true if rotating
        movement.setMode(isRotate);
    }

    //Main Scene Footer
    public void Building()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1){
            icon.image.overrideSprite = clickedImage;
        }
        else{
            SceneManager.LoadScene(1);
            icon.image.overrideSprite = clickedImage;
        }
    }

    //Timing Clock Footer
    public void TimingClock()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2){
            icon.image.overrideSprite = clickedImage;
        }
        else{
            SceneManager.LoadScene(2);
            icon.image.overrideSprite = clickedImage;
        }
    }

    //FriendList Footer
    public void FriendList()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3){
            icon.image.overrideSprite = clickedImage;
        }
        else{
            SceneManager.LoadScene(3);
            icon.image.overrideSprite = clickedImage;
        }
    }

    //LeaderBoard Footer
    public void LeaderBoard()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4){
            icon.image.overrideSprite = clickedImage;
        }
        else{
            SceneManager.LoadScene(4);
            icon.image.overrideSprite = clickedImage;
        }
    }

    //Profile Icon
    public void XPcode()
    {
        SceneManager.LoadScene(5);
    }

    //Customisation Icon
    public void Customisation()
    {
        SceneManager.LoadScene(6);
    }


    //Buildings
    public void MainBuilding()
    {
        SceneManager.LoadScene(7);
    }

    public void ComputerScienceBuilding()
    {
        SceneManager.LoadScene(8);
    }

    public void GeographyBuilding()
    {
        SceneManager.LoadScene(9);
    }

    public void HistoryBuilding()
    {
        SceneManager.LoadScene(10);
    }

    public void BiologyBuilding()
    {
        SceneManager.LoadScene(11);
    }

}

