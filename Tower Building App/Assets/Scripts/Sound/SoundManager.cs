using UnityEngine.Audio;
using UnityEngine;
using System;


// code base taken from https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys
// tutorial and used to make a more personalised sounds manager for this project

public class SoundManager : MonoBehaviour
{

    public Sound[] sounds;

    public static SoundManager instance;


    void Awake()
    {
        //making sure only one instance of the sound manager is running between scenes
        if (instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
            return;
        }

        //making it so that this object isnt destroyed when switching scenes(allows music to continue through scenes)
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.audio_clip;
           s.source.volume = s.volume; 
           s.source.loop = s.loop;
           s.source.bypassListenerEffects = s.bypassEffects;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void Effects(string name, bool on)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        s.source.bypassListenerEffects = !on;
    }


    void Start()
    {
        Play("intro music");
    }
}
