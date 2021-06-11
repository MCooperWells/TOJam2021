using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MoveablePawnScript
{
    //Battery Parameters

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        energyAmount += energyDecayRate * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ChargingStation")
        {
            Debug.Log("Entering Charging Station");
            energyDecayRate = energyRechargeRate;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ChargingStation")
        {
            Debug.Log("Exiting Charging station");
            energyDecayRate = energyDrainRate;
        }
    }
}
