using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Custom_Bulding_Behaviour : MonoBehaviour{
    public Button Colour_Red, Colour_Green, Colour_Blue, Colour_Yellow, Colour_Purple, Decrease_Height, Increase_Height, Reset_Block;
    public Text points;
    public int points_num;


    // Start is called before the first frame update
    void Start(){
        points_num = 0;
        points.text = points_num.ToString();
        
        Colour_Red.onClick.AddListener(() => change_colour(Colour_Red));
        Colour_Green.onClick.AddListener(() => change_colour(Colour_Green));
        Colour_Blue.onClick.AddListener(() => change_colour(Colour_Blue));
        Colour_Yellow.onClick.AddListener(() => change_colour(Colour_Yellow));
        Colour_Purple.onClick.AddListener(() => change_colour(Colour_Purple));

        Decrease_Height.onClick.AddListener(Make_Shorter);
        Increase_Height.onClick.AddListener(Make_Taller);
        Reset_Block.onClick.AddListener(Reset);
    }

    void change_colour(Button button){
        GetComponent<Renderer>().material.color = button.GetComponent<Image>().color;
    }

    void Make_Shorter(){
        points_num += 5;
        points.text = points_num.ToString();
        if (transform.localScale == new Vector3(1,1,1)){
            Debug.Log("Can't get any shorter than this!");
        } else  {
            var change = new Vector3(0,-1,0);
            transform.localScale += change;
            transform.position += (change/2);
        }
    }
    
    void Make_Taller(){
        points_num += 5;
        points.text = points_num.ToString();
        var change = new Vector3(0,1,0);
        transform.localScale += change;
        transform.position += (change/2);
    }

    void Reset(){
        transform.localScale = new Vector3(1,1,1);
        transform.position = new Vector3(0,1,0);
    }
}
