using UnityEngine;

public class GameboyScript : MoveablePawnScript
{
    public GameObject batteryRef1;
    public GameObject batteryRef2;

    private BatteryScript batteryScriptRef1;
    private BatteryScript batteryScriptRef2;

    private bool battery1Close = false;
    private bool battery2Close = false;

    private Light chargingLight;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        batteryScriptRef1 = batteryRef1.GetComponent<BatteryScript>();
        batteryScriptRef2 = batteryRef2.GetComponent<BatteryScript>();

        chargingLight = GetComponent<Light>();
        chargingLight.intensity = 0f;
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

    public void NoBatteryPower(string tag)
    {
        CheckCurrentBatteries(tag, false);
    }

    private void CheckCurrentBatteries(string tag, bool entering)
    {
        switch (tag)
        {
            case "Battery1":
                battery1Close = entering;
                break;

            case "Battery2":
                battery2Close = entering;
                break;

            default:
                break;
        }

        if (battery1Close && batteryScriptRef1.bCanMove || battery2Close && batteryScriptRef2.bCanMove)
        {
            energyDecayRate = energyRechargeRate;
            chargingLight.intensity = 30;
        }
        else
        {
            energyDecayRate = energyDrainRate;
            chargingLight.intensity = 0;
        }

    }
}
