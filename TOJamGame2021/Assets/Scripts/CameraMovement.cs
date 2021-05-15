using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //List of the game objects from the level
    public GameObject[] cameraTransformLocations;

    //Private list of the vector3s that store the location of where the camera will move to
    private Vector3[] cameraVectorLocations;


    //The next location the camera is going to move towards
    private int nextLocation = 0;

    //Pause camera movement
    private bool bMoveCamera;

    //Timer
    private float timer = 0.0f;

    //Camera speed and duration. Both affec the speed the camera moves and how long it has to move
    public float cameraSpeed;
    public float cameraPauseDuration;

    // Start is called before the first frame update
    void Start()
    {
        //Set the camera vector2 array to be as long as the transform location arrya
        cameraVectorLocations = new Vector3[cameraTransformLocations.Length];

        //Collect the X, Z locations and add them to the Vector2
        for (int i = 0; i < cameraTransformLocations.Length; i++)
        {
            cameraVectorLocations[i] = new Vector3(
                cameraTransformLocations[i].transform.position.x, 
                this.gameObject.transform.position.y,
                cameraTransformLocations[i].transform.position.z);
            Debug.Log("Camera Positions number " + (i+1) + " (" + cameraVectorLocations[i].x + ", " + cameraVectorLocations[i].y + ")");
        }

        //Set the location of the camera to be at the first location
        this.gameObject.transform.position = cameraVectorLocations[nextLocation];

        bMoveCamera = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Move Camera bool = " + bMoveCamera + " and Timer =" + timer);
        if (bMoveCamera == false)
        {
            timer += Time.deltaTime;
            if(timer > cameraPauseDuration)
            {
                bMoveCamera = true;
                timer = 0f;
            }
        }

        //If the timer is above 0 (started from -3 means there is a 3 second count down)
        /*if(timer > 0.0f)
        {
            //If the timer is less than 2x the camera duration, then the next location is 1
            if (timer < (2.0f * cameraDuration))
            {
                nextLocation = 1;
            }

            //If the timer is less than 4x the camera duration, then move to location 2
            else if (timer < (4.0f * cameraDuration))
            {
                nextLocation = 2;
            }

            //If the timer is less than 6x the camera duration, then move to location 3
            else if (timer < (6.0f * cameraDuration))
            {
                nextLocation = 3;
            }

            //If the timer is less than 8x the camera duration, then move to location 0 (back to the start)
            else if (timer < (8.0f * cameraDuration))
            {
                nextLocation = 0;
            }

            //If the timer is greater than than 8x the camera duration, then reset the timer
            else if (timer > (8.0f * cameraDuration))
            {
                timer = 0.0f;
            }
        }*/
    }

    void FixedUpdate()
    {
        if(this.gameObject.transform.position == cameraVectorLocations[nextLocation])
        {
            bMoveCamera = false;
            nextLocation++;
            if(nextLocation > 3)
            {
                nextLocation = 0;
            }
        }    
        
        if(bMoveCamera)
        {
            //Moving the camera around
            this.gameObject.transform.position =
                Vector3.MoveTowards(this.gameObject.transform.position,
                new Vector3(
                    cameraVectorLocations[nextLocation].x,
                    this.gameObject.transform.position.y,
                    cameraVectorLocations[nextLocation].z 
                    ),
                cameraSpeed * Time.deltaTime);
        }
    }
}