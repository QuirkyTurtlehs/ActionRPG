using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpin : MonoBehaviour
{
    BoxCollider spinCollider;

    Vector3 defaultSize;

    Quaternion defaultRotation;
    Vector3 defaultPos;

    bool shouldRotate = false;

    public float spinTime = 0.09f;

    public float radius = 2f;

    Vector3 rotVelocity = Vector3.zero;

    Vector3 sizeVelocity = Vector3.zero;

    Vector3 capsulePoint;

    [SerializeField] Material capsuleMaterial;

    void Start()
    {
        //spinCollider = GetComponent<BoxCollider>();
        //spinCollider.enabled = false;
        defaultRotation = transform.rotation;
        defaultPos = transform.position;
        //defaultSize = spinCollider.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldRotate)
        {
            Quaternion currentRot = transform.localRotation;
            Vector3 targetRot = new Vector3(0, 270f, 0);
            //Vector3 targetSize = new Vector3(spinCollider.size.x, spinCollider.size.y, 0.8f);

            //transform.localRotation = Quaternion.Euler(Vector3.SmoothDamp(transform.localRotation.eulerAngles, targetRot, ref rotVelocity, spinTime));

            transform.RotateAround(transform.parent.transform.position, Vector3.up, 2.5f);

            //spinCollider.size = Vector3.SmoothDamp(spinCollider.size, targetSize, ref sizeVelocity, spinTime);
            //if (spinCollider.size.z > 0.2f)
            //{
            //    spinCollider.enabled = true;
            //}

            capsulePoint = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

            RaycastHit[] hits = Physics.CapsuleCastAll(transform.position, capsulePoint, radius, Vector3.up);                    
            

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits.Length > 0)
                {
                    Debug.Log("eyyyyyyyy");
                }   

                if (hits[i].collider.gameObject.tag == "Enemy")
                {
                    EnemyKnockback enemyKnockback = hits[i].collider.gameObject.GetComponent<EnemyKnockback>();
                    enemyKnockback.OnHit(transform.parent.forward);   
                }
            }

            //OnDrawGizmosSelected();
        
        }
        else
        {
            
        }
       
    }
   
    public void SpinActivated()
    {        
        shouldRotate = true;
       
    }
    public void SpinDeactivated()
    {
        shouldRotate = false;
        transform.localRotation = defaultRotation;
        transform.localPosition = new Vector3(0, 0.5f, -0.5f);
        //spinCollider.size = defaultSize;
        //spinCollider.enabled = false;
    }
}
