using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePawnScript : MonoBehaviour
{
    private float energyAmount = 1.0f;
    protected float energyDecayRate = -0.01f;

    public float energyRechargeRate = 0.03f;
    public float energyDrainRate = -0.01f;

    protected bool bCanMove = true;

    protected void Start()
    {
        energyDecayRate = energyDrainRate;
    }


    protected void Update()
    {
        EnergyDrain();
        //Based on the energy amount, either allow the object to move or stop it
        if (energyAmount == 0.0f)
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

    public float GetEenergyAmount()
    {
        return energyAmount;
    }

    public void EnergyDrain()
    {
        //Update the energy decay rate of the pawn
        energyAmount += energyDecayRate * Time.deltaTime;

        //Clamp the energy amount to 1 (100%)
        if (energyAmount > 1.0f)
        {
            energyAmount = 1.0f;
        }
        //Clamp the energy amount to 0 (0%)
        else if (energyAmount < 0.0f)
        {
            energyAmount = 0.0f;
        }
    }
}
