using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStates : MonoBehaviour
{
    DamageStateEffects stateEffects;

    damageState state;

    void Start()
    {
        stateEffects = GetComponent<DamageStateEffects>();

        state = damageState.None;
    }

    void DamageStateController()
    {
        if (state == damageState.None)
            stateEffects.WeakenPlayer();

        if (state == damageState.Weaken)
            stateEffects.InjurePlayer();

        if (state == damageState.Ravaged)
            stateEffects.RavagePlayer();

        if (state == damageState.Dead)
            stateEffects.KillPlayer();
    }   

    public void ChangeDamageState(int strength)
    {
        state = state + strength;

        if (state + strength < damageState.None)
            state = damageState.None;
        
        else if (state + strength > damageState.Dead)
            state = damageState.Dead;

        DamageStateController();
    }
}

public enum damageState
{
    None,
    Weaken,
    Injured,
    Ravaged,
    Dead   
}