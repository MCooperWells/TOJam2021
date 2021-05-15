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
    void Start()
    {
        //Get the game manager's level
        SetGameLevel(GameManager.gameLevel);

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
        Debug.Log("HIT!");
        if (collision.gameObject.tag == "KillZone")
        {
            Die();
            Debug.Log("HIT!");
        }
    }

    //When the player dies, what happens
    void Die()
    {
        Destroy(this.gameObject);
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
