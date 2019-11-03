using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame updat
    public GameObject target;

    public Vector3 offset;

    [SerializeField] float smoothTime = 0.5f;

    [SerializeField] Vector3 velocity = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        if (target != null)
        {  
            FollowTarget(target);
        }

    }

    void FollowTarget(GameObject target)
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + offset, ref velocity,smoothTime);        
    }
}
