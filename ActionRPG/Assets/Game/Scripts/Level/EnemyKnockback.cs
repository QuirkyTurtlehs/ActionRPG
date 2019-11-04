using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;

    public float forceMagnitude;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("kys");
        if (collision.gameObject.tag == "Spin")
        {
            rb.AddForce(Vector3.back * forceMagnitude);
        }
    }
}
