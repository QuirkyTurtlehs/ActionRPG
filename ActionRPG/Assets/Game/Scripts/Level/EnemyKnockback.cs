using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;

    public float forceMagnitude;

    bool hasBeenHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit(Vector3 direction)
    {
        Debug.Log("wow");
        if (!hasBeenHit)
        {
            rb.AddForce(direction * forceMagnitude);
            hasBeenHit = true;
        }
        
    }
    
}
