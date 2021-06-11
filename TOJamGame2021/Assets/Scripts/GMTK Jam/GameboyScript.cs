using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameboyScript : MoveablePawnScript
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        energyAmount += energyDecayRate * Time.deltaTime;
        Debug.Log("Energy amount " + energyAmount);
    }
}
