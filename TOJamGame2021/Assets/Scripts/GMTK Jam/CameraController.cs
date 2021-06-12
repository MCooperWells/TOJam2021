using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothTime = 0.3f;
    
    private Transform playerTransform;
    private Vector3 velocity = Vector3.zero;

    private int activePlayer;
    private int maxPlayer;


    // Start is called before the first frame update
    void Start()
    {

    }    


    // Update is called once per frame
    void Update()
    {
        Vector3 goalPos = playerTransform.position;
        this.transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);
    }

    public void UpdateCamera(GameObject newPlayer)
    {
        playerTransform = newPlayer.transform;
    }    
}
