using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public RaycastHit hit;


    // Update is called once per frame
    void Update()
    {
        Ray camRay = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(camRay, out hit)){
            //Debug.Log("ray hit: " + hit.point);
        }else{
            //Debug.Log("no hit");
        }
    }
}
