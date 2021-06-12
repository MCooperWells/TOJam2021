using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePawnScript : MonoBehaviour
{
    public float energyAmount = 1.0f;
    protected float energyDecayRate = -0.01f;

    public float energyRechargeRate = 0.03f;
    public float energyDrainRate = -0.01f;

    public bool bCanMove = true;

    protected void Start()
    {
        energyDecayRate = energyDrainRate;
    }


    protected void Update()
    {
        //Update the energy decay rate of the pawn
        energyAmount += energyDecayRate * Time.deltaTime;
        
        //Clamp the energy amount to 1 (100%)
        if(energyAmount > 1.0f)
        {
            energyAmount = 1.0f;
        }
        //Clamp the energy amount to 0 (0%)
        else if (energyAmount < 0.0f)
        {
            energyAmount = 0.0f;
        }

        //Based on the energy amount, either allow the object to move or stop it
        if (energyAmount < 0.01f)
        {
            bCanMove = false;
        }
        else
        {
            bCanMove = true;
        }
    }

    public bool CanMove()
    {
        return bCanMove;
    }
}
