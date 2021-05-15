using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : MonoBehaviour
{
    public float speed, height;
    public bool upDown, sideToSideX, sideToSideZ;
    private Vector3 pos;
    // Start is called before the first frame update

    void Start()
    {
        pos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {


        if(upDown == true)
        {

            float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        if(sideToSideX == true)
        {
            float newX = Mathf.Sin(Time.time * speed) * height + pos.x;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        }

        if(sideToSideZ == true)
        {
            float newZ = Mathf.Sin(Time.time * speed) * height + pos.z;
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
    }
}
