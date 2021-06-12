using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUIDisplay : MonoBehaviour
{
    private Slider slider;
    public GameObject energyObject;
    private MoveablePawnScript energyScript;

    // Start is called before the first frame update
    void Start()
    {
        energyScript = energyObject.GetComponent<MoveablePawnScript>();
        slider = GetComponent<Slider>();
        slider.value = energyScript.GetEenergyAmount();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = energyScript.GetEenergyAmount();
    }
}
