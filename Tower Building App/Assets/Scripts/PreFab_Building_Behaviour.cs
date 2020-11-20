using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreFab_Building_Behaviour : MonoBehaviour{
    public Button Brick_Red, Brick_Green, Brick_Blue, Brick_Yellow, Brick_Purple;
    public Button Window_Red, Window_Green, Window_Blue, Window_Yellow, Window_Purple;
    public Button Reset;
    public Color original_brick;
    public Color original_window;

    void Start(){
        original_brick = GetComponent<Renderer>().materials[0].color;
        original_window = GetComponent<Renderer>().materials[1].color;
        
        Brick_Red.onClick.AddListener(() => change_brick_colour(Brick_Red));
        Brick_Green.onClick.AddListener(() => change_brick_colour(Brick_Green));
        Brick_Blue.onClick.AddListener(() => change_brick_colour(Brick_Blue));
        Brick_Yellow.onClick.AddListener(() => change_brick_colour(Brick_Yellow));
        Brick_Purple.onClick.AddListener(() => change_brick_colour(Brick_Purple));

        Window_Red.onClick.AddListener(() => change_window_colour(Window_Red));
        Window_Green.onClick.AddListener(() => change_window_colour(Window_Green)); 
        Window_Blue.onClick.AddListener(() => change_window_colour(Window_Blue));
        Window_Yellow.onClick.AddListener(() => change_window_colour(Window_Yellow));
        Window_Purple.onClick.AddListener(() => change_window_colour(Window_Purple));

        Reset.onClick.AddListener(reset_building);
    }

    void change_brick_colour(Button button){
        GetComponent<Renderer>().materials[0].color = button.GetComponent<Image>().color;
    }

    void change_window_colour(Button button){
        GetComponent<Renderer>().materials[1].color = button.GetComponent<Image>().color;
    }

    void reset_building(){
        GetComponent<Renderer>().materials[0].color = original_brick;
        GetComponent<Renderer>().materials[1].color = original_window;
    }
}
