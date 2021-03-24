using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xpInfo : MonoBehaviour
{   
    public Button PanelButton;
    public GameObject Panel;
    public Button InfoButton;
    void Start()
    {
        PanelButton.onClick.AddListener(() => HideInfo());
        InfoButton.onClick.AddListener(() => ShowInfo());
    }

    void ShowInfo(){
        Panel.SetActive(true);
    }

    void HideInfo(){
        Panel.SetActive(false);
    }

}
