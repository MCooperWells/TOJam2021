using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedMovingObstacle : MovingObstacles
{
    public GameObject[] locationsToMoveGameObject;
    private Vector3[] locationsToMoveVector;
    private int nextLocation;
    private int ArrayLength;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        //Force all side to side movement to be false
        upDown = sideToSideZ= sideToSideX = false;


        locationsToMoveVector = new Vector3[locationsToMoveGameObject.Length];

        for (int i = 0; i < locationsToMoveGameObject.Length; i++)
        {
            locationsToMoveVector[i] = locationsToMoveGameObject[i].transform.position;
        }

        ArrayLength = locationsToMoveGameObject.Length;

        this.gameObject.transform.position = locationsToMoveVector[0];
        nextLocation = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.gameObject.transform.position == locationsToMoveVector[nextLocation])
        { 
            nextLocation++;
            if (nextLocation >= ArrayLength)
            {
                nextLocation = 0;
            }
        }

        //Moving the camera around
        this.gameObject.transform.position =
            Vector3.MoveTowards(this.gameObject.transform.position,
            new Vector3(
                locationsToMoveVector[nextLocation].x,
                this.gameObject.transform.position.y,
                locationsToMoveVector[nextLocation].z
                ),
            speed * Time.deltaTime);
    }
}
