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
    private GameObject playerPawn;
    private GameObject battery1Pawn;
    private GameObject battery2Pawn;

    private MoveablePawnScript playerPawnScript;
    private MoveablePawnScript battery1PawnScript;
    private MoveablePawnScript battery2PawnScript;

    //RB component
    private Rigidbody playerRB;
    private Rigidbody Battery1RB;
    private Rigidbody Battery2RB;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("CameraRoot");

        //Get the player and batteries
        playerPawn = GameObject.FindGameObjectWithTag("Player");
        battery1Pawn = GameObject.FindGameObjectWithTag("Battery1");
        battery2Pawn = GameObject.FindGameObjectWithTag("Battery2");

        //Get scripts
        playerPawnScript = playerPawn.GetComponent<MoveablePawnScript>();
        battery1PawnScript = battery1Pawn.GetComponent<MoveablePawnScript>();
        battery2PawnScript = battery2Pawn.GetComponent<MoveablePawnScript>();

        //Setup the camera and rigid bodies
        mainCamera.GetComponent<CameraController>().UpdateCamera(playerPawn);
        playerRB = playerPawn.GetComponent<Rigidbody>();
        Battery1RB = battery1Pawn.GetComponent<Rigidbody>();
        Battery2RB = battery2Pawn.GetComponent<Rigidbody>();

        playerCanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpacebarPress();
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            ShiftPress();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            CtrlPress();
        }

        //movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        if ((Input.GetAxisRaw("Horizontal") != 0.0f || Input.GetAxisRaw("Vertical") != 0.0f) && playerCanMove)
        {
            playerPawn.transform.rotation = Quaternion.LookRotation(moveInput);
            //animator.SetBool("IsMoving", true);
            //StartWalkingSFX(true, 0);
        }
        else
        {
            //animator.SetBool("IsMoving", false);
            //StartWalkingSFX(false, 0);
        }
        moveVelocity = moveInput * playerSpeed;
    }
    
    //Fixed Updated
    void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + moveVelocity * Time.deltaTime);  
    }

    protected void SpacebarPress()
    {
        playerPawnScript.SpacebarActionEvent();
    }

    protected void ShiftPress()
    {
        Debug.Log("Shift Press!");
        playerPawnScript.ShiftActionEvent();
    }

    protected void CtrlPress()
    {
        Debug.Log("Ctrl Press!");
        playerPawnScript.CtrlActionEvent();
    }
}
