using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorHand : MonoBehaviour
{

    public GameObject Player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 vecToQuat = transform.localRotation.eulerAngles;
        //Vector3 vecTargetRotation = -Player.transform.localRotation.eulerAngles;

        //vecTargetRotation.x = vecToQuat.x;
        //vecTargetRotation.z = vecToQuat.z;
         

        //Quaternion TargetRotation = Quaternion.Euler(vecTargetRotation);

        //transform.localRotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, 10f);

    }
}
