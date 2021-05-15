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
    private int nextLocation = 1;
    private int startingLocation = 0;

    //Pause camera movement
    private bool bMoveCamera;

    //player status
    public bool bIsPlayerAlive;
    
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

    private int NumberLaps = 0;

    // Start is called before the first frame update
    public GameObject nextLevelMenu;

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
        this.gameObject.transform.position = cameraVectorLocations[startingLocation];

        bMoveCamera = false;
        bIsPlayerAlive = true;
        nextLevelMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Move Camera bool = " + bMoveCamera + " and Timer =" + timer);
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
        if(bIsPlayerAlive)
        {
            if (this.gameObject.transform.position == cameraVectorLocations[startingLocation] && nextLocation == 0)
            {
                LevelComplete();
                NumberLaps++;
                Debug.Log("Number of Laps Completed " + NumberLaps);
            }


            if (this.gameObject.transform.position == cameraVectorLocations[nextLocation])
            {
                bMoveCamera = false;
                nextLocation++;
                if (nextLocation > 3)
                {
                    nextLocation = 0;
                }
            }
           
            if (bMoveCamera)
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

    void LevelComplete()
    {
        bIsPlayerAlive = false;
        nextLevelMenu.SetActive(true);
        Debug.Log("You beat the level!!");
    }
}