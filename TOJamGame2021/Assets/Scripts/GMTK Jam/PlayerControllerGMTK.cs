using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerGMTK : MonoBehaviour
{
    //Public player speed, determines how fast the player moves
    public float playerSpeed;
    public float batterySpeed;

    //Public camera
    private GameObject mainCamera;

    //Can the player move?
    private bool playerCanMove;

    //The move velocity of the player as determine by the input
    private Vector3 moveVelocity;

    //array of players and their moveable scripts
    private GameObject[] playerPawns;
    private MoveablePawnScript[] playerPawnScripts;

    //RB component
    private Rigidbody rigidBody;

    //Number of players
    private int activePlayer = 0;
    private int numberOfPawns = 3;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("CameraRoot");

        //Set up the player pawn and scripts
        playerPawns = new GameObject[numberOfPawns];
        playerPawnScripts = new MoveablePawnScript[numberOfPawns];

        //Get the player and batteries
        playerPawns[0] = GameObject.FindGameObjectWithTag("Player");
        playerPawns[1] = GameObject.FindGameObjectWithTag("Battery1");
        playerPawns[2] = GameObject.FindGameObjectWithTag("Battery2");

        //Loop through the pawns and link their scripts
        for (int i = 0; i < playerPawns.Length; i++)
        {
            playerPawnScripts[i] = playerPawns[i].GetComponent<MoveablePawnScript>();
        }

        //Setup the camera and rigid bodies
        mainCamera.GetComponent<CameraController>().UpdateCamera(playerPawns[activePlayer]);
        rigidBody = playerPawns[activePlayer].GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpacebarPress();
        }

            //movement input
            Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        //Get if the current player can move
        playerCanMove = playerPawnScripts[activePlayer].CanMove();

        if ((Input.GetAxisRaw("Horizontal") != 0.0f || Input.GetAxisRaw("Vertical") != 0.0f) && playerCanMove)
        {
            playerPawns[activePlayer].transform.rotation = Quaternion.LookRotation(moveInput);
            //animator.SetBool("IsMoving", true);
            //StartWalkingSFX(true, 0);
        }
        else
        {
            //animator.SetBool("IsMoving", false);
            //StartWalkingSFX(false, 0);
        }

        if(activePlayer == 0)
        {
            moveVelocity = moveInput.normalized * playerSpeed;
        }
        else
        {
            moveVelocity = moveInput.normalized * batterySpeed;
        }
        

        //Changing Characters:
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            activePlayer = 0;
            rigidBody = playerPawns[activePlayer].GetComponent<Rigidbody>();
            mainCamera.GetComponent<CameraController>().UpdateCamera(playerPawns[activePlayer]);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            activePlayer = 1;
            rigidBody = playerPawns[activePlayer].GetComponent<Rigidbody>();
            mainCamera.GetComponent<CameraController>().UpdateCamera(playerPawns[activePlayer]);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            activePlayer = 2;
            rigidBody = playerPawns[activePlayer].GetComponent<Rigidbody>();
            mainCamera.GetComponent<CameraController>().UpdateCamera(playerPawns[activePlayer]);
        }
    }
    
    //Fixed Updated
    void FixedUpdate()
    {
        if(playerCanMove)
        {
            rigidBody.MovePosition(rigidBody.position + moveVelocity * Time.deltaTime);
        }
        
    }

    protected void SpacebarPress()
    {
        playerPawnScripts[activePlayer].SpacebarActionEvent();
    }
}
