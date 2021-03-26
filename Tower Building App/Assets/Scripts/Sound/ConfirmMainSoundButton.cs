using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmMainSoundButton : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ConfirmSound);
    }
    public void ConfirmSound()
    {
        FindObjectOfType<SoundManager>().Play("standard button click");
        //turning the filter off and setting its frequency
        FindObjectOfType<ListenerPersist>().toggleFilterOn(false);
        FindObjectOfType<ListenerPersist>().setFilterFrequency(500);
    }
}
