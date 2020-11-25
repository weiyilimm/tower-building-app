using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FooterIcon : MonoBehaviour
{
    public void Building()
    {
        SceneManager.LoadScene(1);
    }
    public void TimingClock()
    {
        SceneManager.LoadScene(2);
    }
    public void FriendList()
    {
        SceneManager.LoadScene(3);
    }
    public void LeaderBoard()
    {
        SceneManager.LoadScene(4);
    }
}
