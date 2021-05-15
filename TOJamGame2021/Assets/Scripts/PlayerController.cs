using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public player speed, determines how fast the player moves
    private float playerSpeed;

    public float playerSpeedlevel1;
    public float playerSpeedlevel2;
    public float playerSpeedlevel3;

    //Rigidbody used for adding movement
    private Rigidbody rigidBody;

    //The move velocity of the player as determine by the input
    private Vector3 moveVelocity;

    // Start is called before the first frame update
    public GameObject ingameMenu;

    public GameObject cameraObject;
    
    void Start()
    {
        //Get the game manager's level
        SetGameLevel(GameManager.gameLevel);

        ingameMenu.SetActive(false);

        //Get the rigidbody
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        

        moveVelocity = moveInput.normalized * playerSpeed;
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + moveVelocity * Time.deltaTime);
    }

    //When the player hits a collider
    private void OnTriggerEnter(Collider collision)
    {
        //Hitting the kill zone
        if (collision.gameObject.tag == "KillZone")
        {
            Die();
            Debug.Log("HIT!");
        }

        //Hitting a coin
        if(collision.gameObject.tag == "Coin")
        {
            collision.gameObject.SetActive(false);
            Debug.Log("Coin Collected");
            GameManager.Singleton.AddToGameScore(1);
        }

        //Hitting spikes
        if(collision.gameObject.tag == "Spikes")
        {
            Die();
            Debug.Log("Player killed by spikes!");
        }
    }

    //When the player dies, what happens
    void Die()
    {
        ingameMenu.SetActive(true);
        CameraMovement cam = cameraObject.GetComponent<CameraMovement>();
        cam.bIsPlayerAlive = false;
        this.gameObject.SetActive(false);
    }

    //Setting the game level
    void SetGameLevel(int level)
    {
        switch (level)
        {
            case 1:
                playerSpeed = playerSpeedlevel1;

                break;
            case 2:
                playerSpeed = playerSpeedlevel2;

                break;
            case 3:
                playerSpeed = playerSpeedlevel3;

                break;
            default:
                playerSpeed = playerSpeedlevel1;
                break;
        }
    }
}
