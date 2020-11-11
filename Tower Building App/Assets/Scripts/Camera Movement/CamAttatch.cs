using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAttatch : MonoBehaviour
{
    public GameObject player;
/*
    private Vector3 target;
    private Vector3 vel;
    
    // should be: change in percentage between height==5 and height==10, divided by 5;
    // at height==10 angle must be 45..... or not
    private float rotationSpeed = 3;
    private float minAngle = 30;
    private float maxAngle = 45;
*/


    // Update is called once per frame
    void Update()
    {
        //attach camera's position to GameObject player
        transform.position = player.transform.position;

        //adjust camera angle
        //camAngle();
    }
}
    //source for camAngle function: https://answers.unity.com/questions/1219495/how-to-move-or-rotate-main-camera.html
/*
    void camAngle()
    {
        //sets var rotation to current eulerAngles
        Vector3 rotation = transform.eulerAngles;
        float height = transform.position.y;
        
        target.x = 0;
        target.y = minAngle + rotationSpeed*(height-5);
        target.z = 0;
        vel = Vector3.zero;
        if(rotation.x < maxAngle && rotation.x > minAngle){
            rotation.x = minAngle + rotationSpeed*(height-5);
        }else{
            if(rotation.x >= maxAngle && height < 10){
                rotation.x = minAngle + rotationSpeed*(height-5);
            }

            if(rotation.x <= minAngle && height > 5){
                rotation.x = minAngle + rotationSpeed*(height-5);
            }
        }
        transform.eulerAngles = rotation;
    }
}
*/

//old code based off keyboard inputs
//changes rotation.x value, to rotate around x axis, use up/down or w/s to control
        //if else statement/s keep angle within a reasonable range for this project
        //if(rotation.x < 88 && rotation.x > 10)
        //{
        //   rotation.x += Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime; 
        //}
        //else{
        //    if(rotation.x >= 88 && Input.GetAxis("Vertical")<0)
        //    {
        //        rotation.x += Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime; 
        //    }
        //    if(rotation.x <=10 && Input.GetAxis("Vertical")>0)
        //   {
        //        rotation.x += Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime; 
        //    }
        //}
