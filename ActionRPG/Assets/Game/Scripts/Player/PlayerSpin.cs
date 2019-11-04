using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpin : MonoBehaviour
{
    public GameObject spinGm;

    BoxCollider spinCollider;

    Vector3 defaultSize;

    Quaternion defaultRotation;
    bool shouldRotate = false;

    public float spinTime = 0.09f;
    Vector3 rotVelocity = Vector3.zero;

    Vector3 sizeVelocity = Vector3.zero;

    void Start()
    {
        spinCollider = spinGm.GetComponent<BoxCollider>();
        spinCollider.enabled = false;
        defaultRotation = spinGm.transform.rotation;
        defaultSize = spinCollider.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldRotate)
        {
            Quaternion currentRot = spinCollider.transform.localRotation;
            Vector3 targetRot = new Vector3(0, 270f, 0);
            Vector3 targetSize = new Vector3(spinCollider.size.x, spinCollider.size.y, 0.8f);

            spinGm.transform.localRotation = Quaternion.Euler(Vector3.SmoothDamp(spinGm.transform.localRotation.eulerAngles, targetRot, ref rotVelocity, spinTime));

            spinCollider.size = Vector3.SmoothDamp(spinCollider.size, targetSize, ref sizeVelocity, spinTime);
            if (spinCollider.size.z > 0.2f)
            {
                spinCollider.enabled = true;
            }
        }
    }

    public void SpinActivated()
    {        
        shouldRotate = true;      
    }
    public void SpinDeactivated()
    {
        shouldRotate = false;
        spinGm.transform.localRotation = Quaternion.Euler(0,0,0);
        spinCollider.size = defaultSize;
        spinCollider.enabled = false;
    }
}
