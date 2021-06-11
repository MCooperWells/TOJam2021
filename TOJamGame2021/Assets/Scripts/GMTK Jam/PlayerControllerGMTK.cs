using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerGMTK : MonoBehaviour
{
    //Public player speed, determines how fast the player moves
    public float playerSpeed;

    private bool playerCanMove;

    //The move velocity of the player as determine by the input
    private Vector3 moveVelocity;

    //array of players
    public GameObject[] playerPawns;
    private MoveablePawnScript[] playerPawnScripts;
    public Camera mainCamera;

    //RB component
    private Rigidbody rigidBody;

    private int activePlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.GetComponent<CameraController>().UpdateCamera(playerPawns[activePlayer]);
        rigidBody = playerPawns[activePlayer].GetComponent<Rigidbody>();

        playerPawnScripts = new MoveablePawnScript[playerPawns.Length];

        for (int i = 0; i < playerPawns.Length; i++)
        {
            playerPawnScripts[i] = playerPawns[i].GetComponent<MoveablePawnScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        moveVelocity = moveInput.normalized * playerSpeed;

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
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            activePlayer = 3;
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
}
