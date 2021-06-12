using UnityEngine;

public class GameboyScript : MoveablePawnScript
{
    private BatteryScript batteryScriptRef1;
    private BatteryScript batteryScriptRef2;

    private bool battery1Close = false;
    private bool battery2Close = false;

    private Light screenLight;
    private bool bScreenLightOn;

    public float screenDrainAmount = -0.03f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        batteryScriptRef1 = GameObject.FindGameObjectWithTag("Battery1").GetComponent<BatteryScript>();
        batteryScriptRef2 = GameObject.FindGameObjectWithTag("Battery2").GetComponent<BatteryScript>();

        screenLight = GetComponent<Light>();
        screenLight.intensity = bScreenLightOn ? 30f : 0f;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Battery1" || other.gameObject.tag == "Battery2")
        {
            //Debug.Log("Entering battery space");
            CheckCurrentBatteries(other.gameObject.tag, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Battery1" || other.gameObject.tag == "Battery2")
        {
            //Debug.Log("Exiting battery space");
            CheckCurrentBatteries(other.gameObject.tag, false);
        }
    }

    public void UpdateBatteryPower(string tag, bool hasPower)
    {
        CheckPower();
    }

    public void CheckPower()
    {
        if (battery1Close && batteryScriptRef1.CanMove() || battery2Close && batteryScriptRef2.CanMove())
        {
            
            if(battery1Close && batteryScriptRef1.CanMove())
            {
                energyDecayRate = energyRechargeRate;
                batteryScriptRef1.SetEnergyDrainRate(1);
            }
            if (battery2Close && batteryScriptRef2.CanMove())
            {
                energyDecayRate = energyRechargeRate;
                batteryScriptRef2.SetEnergyDrainRate(1);
            }
        }
        else
        {
            energyDecayRate = energyDrainRate;
        }
    }

    private void CheckCurrentBatteries(string tag, bool entering)
    {
        int tmpInt;
        switch (tag)
        {
            case "Battery1":
                battery1Close = entering;

                tmpInt = entering ? 1 : 0;
                batteryScriptRef1.SetEnergyDrainRate(tmpInt); 

                break;

            case "Battery2":
                battery2Close = entering;

                tmpInt = entering ? 1 : 0;
                batteryScriptRef2.SetEnergyDrainRate(tmpInt);

                break;

            default:
                break;
        }
        CheckPower();
    }

    override public void SpacebarActionEvent()
    {
        bScreenLightOn = !bScreenLightOn;
        screenLight.intensity = bScreenLightOn ? 30f : 0f;
        energyDecayRate += bScreenLightOn ? screenDrainAmount : -screenDrainAmount;   
    }
}
