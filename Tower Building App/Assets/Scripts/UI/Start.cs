using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Start : MonoBehaviour
{
    public void StartGame(){

        //stops intro music and starts main theme
        FindObjectOfType<SoundManager>().Play("start button click");
        FindObjectOfType<SoundManager>().Stop("intro music");
        FindObjectOfType<SoundManager>().Play("theme music");
        //turning the filter off and setting its frequency
        FindObjectOfType<ListenerPersist>().toggleFilterOn(false);
        FindObjectOfType<ListenerPersist>().setFilterFrequency(500);
    }
}
