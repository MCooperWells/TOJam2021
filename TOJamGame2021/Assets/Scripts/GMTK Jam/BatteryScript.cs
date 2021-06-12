using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MoveablePawnScript
{
    //Battery Parameters
    private GameboyScript playerScript;
    private Light poweringLight;

    private bool bAtChargingStation = false;

    private BatteryScript otherBatteryRef;

    private bool otherBatteryClose = false;

    private bool bFollowPlayer = false;

    private GameObject playerRef;

    // Start is called before the first frame update
    void Start()
    {
        //base start
        base.Start();

        //Setup batteryscript references
        if (this.gameObject.tag != "Battery1")
            otherBatteryRef = GameObject.FindGameObjectWithTag("Battery1").GetComponent<BatteryScript>();
        if (this.gameObject.tag != "Battery2")
            otherBatteryRef = GameObject.FindGameObjectWithTag("Battery2").GetComponent<BatteryScript>();

        //Get the player script
        playerRef = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerRef.GetComponent<GameboyScript>();

        //set the charging light to zero
        poweringLight = GetComponent<Light>();
        poweringLight.intensity = 0f;

        SetEnergyDrainRate(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Base function
        base.Update();

        //If the charging station can't move, stop it from moving
        if (!bCanMove)
        {
            UpdateGameboyPower(false);
        }
    }

    private void FixedUpdate()
    {
        if (bFollowPlayer)
        {
            transform.position = Vector3.Lerp(this.transform.position, playerRef.transform.position, Time.deltaTime * 3f);
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

            CheckOtherBatteries(true);
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

            if(otherBatteryClose && otherBatteryRef.bAtChargingStation)
            {
                
            }
            else
            {
                StartCharging(false);
            }

            //Check if there are other batteries nearby
            PowerOtherBatteries(false);
            playerScript.CheckPower();
        }

        //Leaving other batteries
        else if (other.gameObject.tag == "Battery1" || other.gameObject.tag == "Battery2")
        {
            Debug.Log(this.gameObject.tag + " has moved away from " + other.gameObject.tag);

            CheckOtherBatteries(false);
            playerScript.CheckPower();
        }
    }

    private void CheckOtherBatteries(bool entering)
    {
        otherBatteryClose = entering;

        if(!bAtChargingStation)
        {
            if(otherBatteryClose && otherBatteryRef.bAtChargingStation)
            {
                StartCharging(true);
                UpdateGameboyPower(true);
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

    public void UpdateGameboyPower(bool hasPower)
    {
        playerScript.UpdateBatteryPower(this.gameObject.tag, hasPower);
    }

    private void StartCharging(bool charging)
    {
        int tempInt = charging ? 2 : 0;
        
        SetEnergyDrainRate(tempInt);

        poweringLight.intensity = charging ? 30 : 0;
        if (charging)
            bCanMove = true;
    }

    //1 = no decay
    //2 = drain
    //3 = charge
    public void SetEnergyDrainRate(int chargeType)
    {
        if(bAtChargingStation || (otherBatteryClose && otherBatteryRef.bAtChargingStation))
        {
            energyDecayRate = energyRechargeRate;
        }
        else
        {
            switch (chargeType)
            {
                case 0:
                    energyDecayRate = 0.0f;
                    break;
                case 1:
                    energyDecayRate = energyDrainRate;
                    break;
                case 2:
                    energyDecayRate = energyRechargeRate;
                    break;
                default:
                    break;
            }
        }
    }

    public void SetFollowPlayer(bool FollowPlayer)
    {
        bFollowPlayer = FollowPlayer;
    }
}
