using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStateHandle : MonoBehaviour
{
    //Int that determends the players damage state
    int DamageState = 0;

    //Increase or decrease the damage state
    public void ChangeDamageState(int strength)
    {
        DamageState += strength;
    }
    //Getter function for other script that needs DamageState
    public int GetDamageState()
    {
        return DamageState;
    }
}
