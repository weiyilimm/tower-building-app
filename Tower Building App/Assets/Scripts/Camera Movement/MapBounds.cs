using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    //only making bounds for x and z, height bounds are
    //already handled fine without issues
    //so I see no reason to change the old system for height
    private Vector2 mapBounds;

    
    void LateUpdate()
    {
        Vector3 camPos = transform.position;
        //Debug.Log("original: " + camPos);
        //clamp camPos.x to xMin, xMax
        float xInBounds = Mathf.Clamp(camPos.x, -5, 5);

        transform.Translate(xInBounds,camPos.y,camPos.z);
        
        Debug.Log("after clamp: " + xInBounds);
    }
}
