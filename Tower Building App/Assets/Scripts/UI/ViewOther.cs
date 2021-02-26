using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ViewOther : MonoBehaviour
{   
    public GameObject FriendList;
    public GameObject LoadingText;
    public Button FriendName; 
    // Start is called before the first frame update
    void Start()
    {
        FriendName.onClick.AddListener(() => OthersWorld());
    }

    public void OthersWorld()
    {   
        LoadingText.SetActive(true);
        FriendList.SetActive(false);
        SceneManager.LoadScene(16);
    }
}
