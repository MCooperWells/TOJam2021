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
    private bool bIsCharging = false;

    private BatteryScript batteryScriptRef1;
    private BatteryScript batteryScriptRef2;
    private BatteryScript batteryScriptRef3;

    private bool battery1Close = false;
    private bool battery2Close = false;
    private bool battery3Close = false;

    private string stringID;

    // Start is called before the first frame update
    void Start()
    {
        //base start
        base.Start();



        GameObject.FindGameObjectsWithTag("Respawn");


        //Setup batteryscript references
        if (this.gameObject.tag != "Battery1")
            batteryScriptRef1 = GameObject.FindGameObjectWithTag("Battery1").GetComponent<BatteryScript>();
        if (this.gameObject.tag != "Battery2")
            batteryScriptRef2 = GameObject.FindGameObjectWithTag("Battery2").GetComponent<BatteryScript>();
        if (this.gameObject.tag != "Battery3")
            batteryScriptRef3 = GameObject.FindGameObjectWithTag("Battery3").GetComponent<BatteryScript>();

        //Get the player script
        playerScript = player.GetComponent<GameboyScript>();

        //set the charging light to zero
        poweringLight = GetComponent<Light>();
        poweringLight.intensity = 0f;

        //Save the string of the ID
        stringID = this.gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        //Base function
        base.Update();

        //If the charging station can't move, stop it from moving
        if(!bCanMove)
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
            StartCharging();

            //When entering the charging station, check if there are other batteries nearby
            CheckOtherBatteries("", true);
        }


        //Touching other batteries
        else if(other.gameObject.tag == "Battery1" || other.gameObject.tag == "Battery2" || other.gameObject.tag == "Battery3")
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
            StopCharging();

            //Check if there are other batteries nearby
            CheckOtherBatteries("", false);
        }

        //Leaving other batteries
        else if (other.gameObject.tag == "Battery1" || other.gameObject.tag == "Battery2" || other.gameObject.tag == "Battery3")
        {
            Debug.Log(this.gameObject.tag + " has moved away from " + other.gameObject.tag);

            CheckOtherBatteries(other.gameObject.tag, false);
        }
    }

    private void CheckOtherBatteries(string tag, bool entering)
    {
        switch (tag)
        {
            case "Battery1":
                battery1Close = entering;
                break;

            case "Battery2":
                battery2Close = entering;
                break;

            case "Battery3":
                battery3Close = entering;
                break;

            default:
                break;
        }

        switch(this.gameObject.tag)
        {
            case "Battery1":
                if(battery2Close && batteryScriptRef2.bIsCharging || battery3Close && batteryScriptRef3.bIsCharging)
                {
                    StartCharging();
                }
                else
                {
                    StopCharging();
                }

                break;

            case "Battery2":
                if (battery1Close && batteryScriptRef1.bIsCharging || battery3Close && batteryScriptRef3.bIsCharging)
                {
                    StartCharging();
                }
                else
                {
                    StopCharging();
                }
                break;

            case "Battery3":
                if (battery1Close && batteryScriptRef1.bIsCharging || battery2Close && batteryScriptRef2.bIsCharging)
                {
                    StartCharging();
                }
                else
                {
                    StopCharging();
                }
                break;

            default:
                break;
        }

        if(battery1Close)
        {
            batteryScriptRef1.CheckOtherBatteries(this.gameObject.tag, entering);
        }
        if(battery2Close)
        {
            batteryScriptRef2.CheckOtherBatteries(this.gameObject.tag, entering);
        }
        if (battery3Close)
        {
            batteryScriptRef3.CheckOtherBatteries(this.gameObject.tag, entering);
        }
    }

    public void OutOfEnergy()
    {
        playerScript.NoBatteryPower(this.gameObject.tag);
    }

    private void StartCharging()
    {
        bIsCharging = true;
        energyDecayRate = energyRechargeRate;
        poweringLight.intensity = 30;
    }

    private void StopCharging()
    {
        bIsCharging = false;
        energyDecayRate = energyDrainRate;
        poweringLight.intensity = 0;
    }
}
