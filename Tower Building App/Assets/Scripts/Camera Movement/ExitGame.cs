using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Cancel - check input manager
        if(Input.GetKey("escape"))
        {
            //Debug.Log("request to quit");
            Application.Quit();
        }
    }
}
