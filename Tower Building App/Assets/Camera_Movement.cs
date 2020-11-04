using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera_Movement : MonoBehaviour{
    public float speed;
    public Transform target;
    
    // Start is called before the first frame update
    void Start(){
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.W)){
            transform.RotateAround(target.position, transform.right, 45 * speed);
        };
        if (Input.GetKeyDown(KeyCode.A)){
            transform.RotateAround(target.position, transform.up, 45 * speed);
        };
        if (Input.GetKeyDown(KeyCode.S)){
            transform.RotateAround(target.position, transform.right, -45 * speed);
        };
        if (Input.GetKeyDown(KeyCode.D)){
            transform.RotateAround(target.position, transform.up, -45 * speed);
        };
    }
}
