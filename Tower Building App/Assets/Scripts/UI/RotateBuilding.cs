using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBuilding : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 100f;
    private Touch touch;
    Rect rect = new Rect(0, 260, 235, 120);

    private void Update()
    {   
        if (Input.touchCount > 0)
        {   

            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved){
                if (rect.Contains(touch.position)){
                    transform.Rotate(0f,-touch.deltaPosition.x * rotationSpeed, 0f);
                }
            }
        }

    }

    //method to see the limit area of touch rotating

    public void OnGUI()
    {
        GUI.Box(rect, "This is a box");
    }
}



