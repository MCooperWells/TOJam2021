using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePawnScript : MonoBehaviour
{
    public float energyAmount = 1.0f;
    public float energyDecayRate = -0.01f;

    public float energyRechargeRate = 0.03f;
    public float energyDrainRate = -0.01f;

    public bool bCanMove = true;

    public bool CanMove()
    {
        return bCanMove;
    }
}
