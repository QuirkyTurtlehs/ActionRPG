using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    int health = 4;

    public Image healthBar;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int GetPlayerHealth()
    {
        return health;
    }

    public void SetPlayerHealth(int value)
    {
        health += value; 

        
    }
}
