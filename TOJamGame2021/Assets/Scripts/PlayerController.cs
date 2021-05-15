using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public player speed, determines how fast the player moves
    public float playerSpeed;

    //Rigidbody used for adding movement
    private Rigidbody rigidBody;

    //The move velocity of the player as determine by the input
    private Vector3 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
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

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("HIT!");
        if (collision.gameObject.tag == "KillZone")
        {
            Die();
            Debug.Log("HIT!");
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
