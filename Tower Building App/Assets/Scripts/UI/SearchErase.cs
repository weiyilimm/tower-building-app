using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchErase : MonoBehaviour
{   

    public TextMeshProUGUI text;
    public TMP_InputField erase;

    public void Erase(){
        erase.text = "";
    }

}
