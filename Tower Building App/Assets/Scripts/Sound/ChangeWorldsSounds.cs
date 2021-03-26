using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWorldsSounds : MonoBehaviour
{

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(StartButtonClickSound);
    }
    public void StartButtonClickSound()
    {
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turning the filter off and setting its frequency
        FindObjectOfType<ListenerPersist>().toggleFilterOn(false);
        FindObjectOfType<ListenerPersist>().setFilterFrequency(500);
    }
}
