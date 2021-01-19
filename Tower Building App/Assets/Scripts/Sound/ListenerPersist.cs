using UnityEngine;

public class ListenerPersist : MonoBehaviour
{
    public static ListenerPersist instance;

    void Awake()
    {
        //making sure only one instance of the audio Listener is running between scenes
        if (instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
            return;
        }

        //making it so that this object isnt destroyed when switching scenes(allows music to continue through scenes)
        DontDestroyOnLoad(gameObject);
    }


    public void setFilterFrequency(int frequency)
    {
        AudioLowPassFilter filter = FindObjectOfType<AudioLowPassFilter>();
        filter.cutoffFrequency = frequency;
    }


    public void toggleFilterOn(bool on_or_off)
    {
        AudioLowPassFilter filter = FindObjectOfType<AudioLowPassFilter>();
        filter.enabled = on_or_off;
    }

    void Update(){
        if (Input.GetMouseButtonDown(1))
        {
            toggleFilterOn(false);
        }
    }

}
