using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacles : MovingObstacles
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        upDown = sideToSideZ = sideToSideX = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
