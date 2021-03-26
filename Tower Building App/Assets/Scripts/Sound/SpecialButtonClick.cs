using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialButtonClick : MonoBehaviour
{

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(StartButtonClickSound);
    }
    public void StartButtonClickSound()
    {
        FindObjectOfType<SoundManager>().Play("start button click");
    }
}
