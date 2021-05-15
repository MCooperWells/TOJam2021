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
    private float cameraSpeed;
    public float cameraSpeedLevel1;
    public float cameraSpeedLevel2;
    public float cameraSpeedLevel3;

    private float cameraPauseDuration;
    public float cameraPauseDurationLevel1;
    public float cameraPauseDurationLevel2;
    public float cameraPauseDurationLevel3;

    // Start is called before the first frame update
    void Start()
    {
        //Before anything else, set the game level
        SetGameLevel(GameManager.gameLevel);

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

    //Setting the game level 
    void SetGameLevel(int level)
    {
        switch(level)
        {
            case 1:
                cameraSpeed = cameraSpeedLevel1;
                cameraPauseDuration = cameraPauseDurationLevel1;

                break;
            case 2:
                cameraSpeed = cameraSpeedLevel2;
                cameraPauseDuration = cameraPauseDurationLevel2;

                break;
            case 3:
                cameraSpeed = cameraSpeedLevel3;
                cameraPauseDuration = cameraPauseDurationLevel3;

                break;
            default:
                cameraSpeed = cameraSpeedLevel1;
                cameraPauseDuration = cameraPauseDurationLevel1;
                break;
        }
    }
}