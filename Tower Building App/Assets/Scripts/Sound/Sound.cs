using UnityEngine.Audio;
using UnityEngine;

// code base taken from https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys
// tutorial and used to make a more personalised sounds class (used in sound manager) for this project


[System.Serializable]
public class Sound{

    public string name;
    public AudioClip audio_clip;

    [Range(0f,1f)]
    public float volume;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
