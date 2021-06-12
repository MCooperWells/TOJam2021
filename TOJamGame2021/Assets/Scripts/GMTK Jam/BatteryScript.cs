using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MoveablePawnScript
{
    //Battery Parameters
    public GameObject player;
    private GameboyScript playerScript;
    private Light poweringLight;

    private bool bAtChargingStation = false;

    private BatteryScript otherBatteryRef;

    private bool otherBatteryClose = false;

    // Start is called before the first frame update
    void Start()
    {
        //base start
        base.Start();



        GameObject.FindGameObjectsWithTag("Respawn");


        //Setup batteryscript references
        if (this.gameObject.tag != "Battery1")
            otherBatteryRef = GameObject.FindGameObjectWithTag("Battery1").GetComponent<BatteryScript>();
        if (this.gameObject.tag != "Battery2")
            otherBatteryRef = GameObject.FindGameObjectWithTag("Battery2").GetComponent<BatteryScript>();

        //Get the player script
        playerScript = player.GetComponent<GameboyScript>();

        //set the charging light to zero
        poweringLight = GetComponent<Light>();
        poweringLight.intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Base function
        base.Update();

        //If the charging station can't move, stop it from moving
        if (!bCanMove)
        {
            OutOfEnergy();
        }
    }


    //ON TRIGGER ENTER ------------------------------
    private void OnTriggerEnter(Collider other)
    {
        //Touching the charging station
        if (other.gameObject.tag == "ChargingStation")
        {
            //At the charging station
            Debug.Log("Entering Charging Station");
            bAtChargingStation = true;
            StartCharging(true);

            //When entering the charging station, check if there are other batteries nearby
            PowerOtherBatteries(true);
        }


        //Touching other batteries
        else if (other.gameObject.tag == "Battery1" || other.gameObject.tag == "Battery2")
        {
            //Print out when batteries get close
            Debug.Log(this.gameObject.tag + " is close to " + other.gameObject.tag);

            CheckOtherBatteries(other.gameObject.tag, true);
        }
    }

    //ON TRIGGER EXIT ------------------------------
    private void OnTriggerExit(Collider other)
    {
        //Leaving the charging station
        if (other.gameObject.tag == "ChargingStation")
        {
            //Leaving the charging station
            Debug.Log("Exiting Charging station");
            bAtChargingStation = false;
            StartCharging(false);

            //Check if there are other batteries nearby
            PowerOtherBatteries(false);
        }

        //Leaving other batteries
        else if (other.gameObject.tag == "Battery1" || other.gameObject.tag == "Battery2")
        {
            Debug.Log(this.gameObject.tag + " has moved away from " + other.gameObject.tag);

            CheckOtherBatteries(other.gameObject.tag, false);
        }
    }

    private void CheckOtherBatteries(string tag, bool entering)
    {
        otherBatteryClose = entering;

        if(!bAtChargingStation)
        {
            if(otherBatteryClose && otherBatteryRef.bAtChargingStation)
            {
                StartCharging(true);
            }
            else if(otherBatteryClose && !otherBatteryRef.bAtChargingStation)
            {
                StartCharging(false);

            }
            else if(!otherBatteryClose)
            {
                StartCharging(false);

            }
        }
    }

    private void PowerOtherBatteries(bool powering)
    {
        if(otherBatteryClose && !otherBatteryRef.bAtChargingStation)
        {
            otherBatteryRef.StartCharging(powering);

        }
    }

    public void OutOfEnergy()
    {
        playerScript.NoBatteryPower(this.gameObject.tag);
    }

    private void StartCharging(bool charging)
    {
        energyDecayRate = charging ? energyRechargeRate : energyDrainRate;
        poweringLight.intensity = charging ? 30 : 0;
    }
}
