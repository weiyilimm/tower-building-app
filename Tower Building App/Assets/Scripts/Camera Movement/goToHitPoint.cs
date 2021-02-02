using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goToHitPoint : MonoBehaviour
{
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("CameraObject");
        CameraMovePhone camMoveClass = cam.GetComponent<CameraMovePhone>();
        Vector3 hit = camMoveClass.point;

        transform.position = hit;
    }
}
