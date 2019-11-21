using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageStateEffects : MonoBehaviour
{
    //player
    PlayerMovement playerMovement;

    public Image playerHealth;

    //Weaken
    float crowdControllMult = 1f;
    float slowMult = 1f;

    //Injured
    bool isVulnerable = false;

    //Ravaged
    bool isSlowed;
    bool movementLimited = false;

    //Dead
    public GameObject PlayerBody;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void NoDamageEffects()
    {
        crowdControllMult = 1f;
        slowMult = 1f;
        isVulnerable = false;
        isSlowed = false;
        movementLimited = false;

        playerHealth.fillAmount = 1f;
    }

    public void WeakenPlayer()
    {
        crowdControllMult = 1.25f;
        slowMult = 1.15f;

        playerHealth.fillAmount = 0.75f;
    }

    public void InjurePlayer()
    {
        crowdControllMult = 1.5f;
        slowMult = 1.3f;
        isVulnerable = true;

        playerHealth.fillAmount = 0.5f;
    }

    public void RavagePlayer()
    {
        crowdControllMult = 2f;
        slowMult = 1.5f;
        isVulnerable = true;
        isSlowed = true;
        movementLimited = true;

        playerHealth.fillAmount = 0.25f;
    }

    public void KillPlayer()
    {
        Instantiate(PlayerBody, transform.position, transform.rotation);
        gameObject.SetActive(false);

        playerHealth.fillAmount = 0f;
    }
//____________________________________________________________________________________________________

    public float GetCrowdControllMult()
    {
        return crowdControllMult;
    }

    public bool GetIsVunerable()
    {
        return isVulnerable;
    }

    public bool GetIsSlowed()
    {
        return isSlowed;
    }

    public bool GetMovementLimited()
    {
        return movementLimited;
    }

}
