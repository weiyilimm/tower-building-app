using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraMovePhone : MonoBehaviour
{
    //creating variables
    private Vector3 startPos;//for one finger (panning)
    private Vector3 startPos1;//for finger 1 (zooming)

    private Vector3 startPos2;//for finger 2 (zooming)
    private Vector3 dragOrigin = Vector3.zero;//assigning it to vector3 equivalent of null
    private Vector3 dragCurrent;//for one finger (panning)
    private Vector3 dragCurrent1;//for finger 1 (zooming)
    private Vector3 dragCurrent2;//for finger 2 (zooming)
    private Vector3 moved;
    private Vector3 zoomed;
    

    private float moveSpeed = 1;//moveSpeed between 0 - 5 is acceptable
    private float yMoveSpeed = 1f;
    private float distance;//current distance
    private float startDistance;
    private float distanceOrigin;

    //variables for calcluating camera angle change
    GameObject cam;
    CamAttatch camScript;


    //rotation variables
    private bool mode_pan = true;
    
    private Vector3 rotateOrigin;
    private Vector3 rotateCurrent;
    private float rotated = 0;
    private Vector3 point = Vector3.zero;

    //change the movement/rotation setting
    public void setMode(bool b)
    {
        mode_pan = b;
    }


    // Update is called once per frame
    void Update()
    {
        //check if only one finger is touching the screen
        if(Input.touchCount == 1){
            //create Touch object to store info on users touch
            Touch touch = Input.GetTouch(0);

            //if touch object just started moving, store that as the start Pos
            if(touch.phase == TouchPhase.Began){
                startPos = touch.position;

                //set point to rotate around(0.5,0.5 is center of 2d screen, last element is distance from camera (used current height for this))
                // +3 for fun
                if (! mode_pan) {
                    point = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, transform.position.y + 3));
                    //Debug.Log(point);
                }
                
            }

            if(touch.phase == TouchPhase.Moved){
                //movement for pan mode
                if(mode_pan){
                    //if not set,set dragOrigin to the startPos
                    if(dragOrigin == Vector3.zero){
                        dragOrigin = startPos;
                    //else set dragOrigin to previous drag position
                    }else{
                        dragOrigin = dragCurrent;
                    }
                    //update current drag position
                    dragCurrent = touch.position;

                    //store how much to move based on touch positions
                    moved = dragOrigin - dragCurrent;

                    //re-organize moved to match actual spacial movements
                    float x = moved.x * (moveSpeed / 100);
                    float z = moved.y * (moveSpeed / 100);

                    //check if we can pan camera box
                    //minX = -30, maxX = 30, minZ = -32, maxZ = 18;
                    bool[] canPan = canPanWithinBounds(-30,30,-32,18);
                    
                    //if x can pan, pan on x
                    if(canPan[0] == true){
                        transform.Translate(x, 0, 0); //moved.x * (moveSpeed/100),0,0,Space.World);
                    }

                    //if z can pan, pan on z
                    if(canPan[1] == true){
                        transform.Translate(0, 0, z); //0,0,moved.z * (moveSpeed/100),Space.World);
                    }
                }else{
                    //if not set,set rotateOrigin to the startPos
                    if(rotateOrigin == Vector3.zero){
                        rotateOrigin = startPos;
                    //else set rotateOrigin to previous drag position
                    }else{
                        rotateOrigin = rotateCurrent;
                    }
                    //update current drag position
                    rotateCurrent = touch.position;

                    //store how much to move based on touch positions
                    rotated = rotateCurrent.x - rotateOrigin.x;

                    //rotate
                    transform.RotateAround(point,Vector3.up,rotated/5);//replace 5 with some kind of speed variable later
                }
            }

            //reset startPos if user lifts their finger
            if(touch.phase == TouchPhase.Ended){
                dragOrigin = Vector3.zero;
                rotateOrigin = Vector3.zero;
            }
        }
        //
        //      /\
        //     /^^\
        //     ****
        //     *^^*
        //     **** 
        //     *^^*
        //     ****
        //     *^^*
        //     ****
        //     *^^*
        //   ********
        //  **********
        //  **  **  **
        //  *        *   
        //    **  ** 
        //  *   **   *  
        // *  * ** *  *  
        // *   *  *   *  
        //   *  **  *        
        //    * ** *
        //      **
        //
        //check if two fingers are touching the screen
        if(Input.touchCount == 2){
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            //dont need to get check for touch1 as it wont be on began, it will have began in the first if statement
            if(touch2.phase == TouchPhase.Began){
                startPos1 = touch1.position;
                startPos2 = touch2.position;
                
                //get initial distance between two fingers
                startDistance = Distance2D(startPos1,startPos2);
            
                //on the first loop, distance needs set to startDistance, so that distance can pass the correct value to distanceOrigin 
                distance = startDistance;
            }

            //get current positions of fingers after first loop
            if(touch1.phase == TouchPhase.Moved | touch1.phase == TouchPhase.Stationary){
                dragCurrent1 = touch1.position;
            }
            if(touch2.phase == TouchPhase.Moved | touch2.phase == TouchPhase.Stationary){
                dragCurrent2 = touch2.position;
            }

            //set distanceOrigin to old distance
            distanceOrigin = distance;
            //update current distance
            //if its not the first loop, current distance will update
            if(touch2.phase != TouchPhase.Began){
                //distance is calculated off current positions of fingers
                distance = Distance2D(dragCurrent1,dragCurrent2);
            }
            
            //set zoomed up so it changes y
            zoomed.x = 0;
            zoomed.y = distanceOrigin-distance;
            zoomed.z = 0;
            
            //"zoom" camera by making it move up/down on the y axis
            //minY = 5, maxY = 20
            //canZoomWithinBounds checks if "currently" in bounds, however it cant check if the change in zoom will
            if (canZoomWithinBounds(5,20)){
                float changeInHeight = zoomed.y*(yMoveSpeed/100);
                //if changeInHeight won't take you out of bounds, change y pos,
                //otherwise change y pos to max/min and store smaller change in height
                if(transform.position.y + changeInHeight>=5 && transform.position.y + changeInHeight<=20){
                    zoomed.y = changeInHeight;
                }else{
                    //use y = 10 as middle point, assume if above 10, stuck going up, below 10, stuck going down
                    if(transform.position.y > 10){
                        changeInHeight = 20 - transform.position.y;//height change is distance from current y to limit 20
                        zoomed.y = changeInHeight;
                    }
                    if(transform.position.y < 10){
                        changeInHeight = 5 - transform.position.y;
                        zoomed.y = changeInHeight;
                    }
                }
                transform.Translate(zoomed);//transform by change of height in vector3 form/zoomed

                //camera angle change is only smooth when based off rate of change in height
                changeCamAngle(changeInHeight);
            }

            //reset variables to simulate TouchPhase.Began (in panning) incase someone holds on with one finger
            if (touch2.phase == TouchPhase.Ended){
               dragOrigin = Vector3.zero;
               startPos = touch1.position;
            }
            if(touch1.phase == TouchPhase.Ended){
                dragOrigin = Vector3.zero;
                startPos = touch2.position;
            }
            
        }
    }

    bool canZoomWithinBounds(int minY, int maxY){
        bool canZoom = false;
            if (transform.position.y > minY && transform.position.y < maxY)//if within y bounds, change position
            {
                canZoom = true;
            }else{//if not within y bounds
                //check if y is too low, if so, only apply appropriate position changes
                if (transform.position.y <= minY && zoomed.y > 0)
                {
                   canZoom = true;
                }
                //check if y is too high, if so, only apply appropriate position changes
                if (transform.position.y >= maxY && zoomed.y < 0)
                {
                    canZoom = true;
                }
            }
        return canZoom;
    }
    bool[] canPanWithinBounds(int minX, int maxX, int minZ, int maxZ){
        bool[] returnVals = new bool[2];

        //making sure x changes are within x bounds
        if (transform.position.x > minX && transform.position.x < maxX){
                returnVals[0] = true;
        }else{//if current position not within x bounds
            if (transform.position.x <= minX && moved.x > 0)
            {
                returnVals[0] = true;
            }
            //check if x is too high, if so, only apply appropriate position changes
            if (transform.position.x >= maxX && moved.x < 0)
            {
                returnVals[0] = true;
            }
        }

        //making sure z changes are within z bounds
        if (transform.position.z > minZ && transform.position.z < maxZ)//if within z bounds, change position
            {
                returnVals[1] = true;
            }else{//if current position not within z bounds
                if (transform.position.z <= minZ && moved.z > 0)
                {
                    returnVals[1] = true;
                }
                //check if z is too high, if so, only apply appropriate position changes
                if (transform.position.z >= maxZ && moved.z < 0)
                {
                    returnVals[1] = true;
                }
            }
        return returnVals; 
    }

    float Distance2D(Vector3 point1, Vector3 point2){
        //uses the distance formula to find distance between two 2d points
        return Mathf.Abs(Mathf.Sqrt(Mathf.Pow(point1[0]-point2[0],2) + Mathf.Pow(point1[1]-point2[1],2)));
    }

    void changeCamAngle(float changeInHeight){
        //getting the camera object and its scripts
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        camScript = cam.GetComponent<CamAttatch>();

        Vector3 rotation = camScript.transform.eulerAngles;

        if(transform.position.y <= 5){
            rotation.x = 30;
            moveSpeed = 0.8f;

        }

        if(transform.position.y >= 10){
            rotation.x = 45;
            moveSpeed = 2;
        }

        //changing x by rate of change in height
        if(transform.position.y >5 && transform.position.y <10){
            rotation.x += 3*(changeInHeight);
            moveSpeed += 2 * ((changeInHeight) / 14);
        }
        //Debug.Log(moveSpeed);
                
        //updating cameras eulerAngles
        camScript.transform.eulerAngles = rotation;
    }
}