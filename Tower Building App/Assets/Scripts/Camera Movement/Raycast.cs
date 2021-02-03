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
        Physics.Raycast(camRay, out hit);
    }
}
