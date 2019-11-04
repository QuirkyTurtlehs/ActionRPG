using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerInput : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator anim;
    RaycastHit hitInfo = new RaycastHit();

    public GameObject spinObject;

    PlayerSpin playerSpin;

    float refSlideCd = 1f;
    float slideCd = 0;

    float refSpinCd = 2f;
    float spinCd = 0;

    float baseSpeed;
    public float slideSpeed;

    Vector3 nextDestination = Vector3.zero;

    bool isSliding = false;
    bool isSpinning = false;


    Vector3 firstVectorPoint = Vector3.zero;

    public List<SliceVectors> sliceVectorList = new List<SliceVectors>(); 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        playerSpin = spinObject.GetComponent<PlayerSpin>();

        slideCd = refSlideCd;
        spinCd = refSpinCd;

        baseSpeed = agent.speed;
        slideSpeed = baseSpeed * 1.5f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLeftMoveClick();
        }
        if (Input.GetMouseButtonDown(1))
        {
            OnRightClick();
        }
        if (Input.GetMouseButtonUp(1))
        {
            OnRightClickReleased();
        }
        

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (slideCd >= refSlideCd)
            {
                slideCd = 0;
                StartCoroutine(Slide());
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (spinCd >= refSpinCd)
            {
                spinCd = 0;
                StartCoroutine(Spin());
            }
            
        }

        if (slideCd < 1)
        {
            slideCd += Time.deltaTime;
        }
        if (spinCd < 2)
        {
            spinCd += Time.deltaTime;
        }
    }

    void OnRightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
        {
            firstVectorPoint = hitInfo.point;
        }
    }
    void OnRightClickReleased()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
        {
            Vector3 secondVectorPoint = secondVectorPoint = hitInfo.point;
            VectorSlice(firstVectorPoint, secondVectorPoint);
            firstVectorPoint = Vector3.zero;
        }
    }
    void VectorSlice(Vector3 firstPoint, Vector3 secondPoint)
    {
        Ray Ray;
        RaycastHit[] hits = Physics.RaycastAll(firstPoint, secondPoint.normalized, Vector3.Distance(firstPoint, secondPoint));

        SliceVectors sliceVectors = new SliceVectors(firstPoint, secondPoint);
        sliceVectorList.Add(sliceVectors);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.tag == "Enemy")
            {

            }
        }
    }
    void OnLeftMoveClick()
    {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
         {
            if (!isSliding || !isSpinning)
            {
                agent.destination = hitInfo.point;
                nextDestination = Vector3.zero;
            }
            else
            {
                nextDestination = hitInfo.point;
            }
        }
    }
    IEnumerator Slide()
    {
        isSliding = true;
        anim.SetBool("slide", true);

        agent.speed = slideSpeed;

        yield return new WaitForSeconds(1f);
        anim.SetBool("slide", false);
        isSliding = false;
       
        if (nextDestination != Vector3.zero)
        {
            agent.destination = nextDestination;
        }
        agent.speed = baseSpeed;
    }
    IEnumerator Spin()
    {
        isSpinning = true;
        Debug.Log("yaaaaaaaaaaaay");
        anim.SetBool("move", false);
        anim.SetBool("spin", true);
        agent.isStopped = true;

        playerSpin.SpinActivated();
        
        yield return new WaitForSeconds(1f);

        playerSpin.SpinDeactivated();

        anim.SetBool("move", false);
        agent.ResetPath();
        agent.isStopped = false;
        anim.SetBool("spin", false);
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < sliceVectorList.Count; i++)
        {
            Gizmos.DrawLine(sliceVectorList[i].first, sliceVectorList[i].second);
        }        
    }
}

public struct SliceVectors
{
    public Vector3 first;
    public Vector3 second;

    public SliceVectors(Vector3 firstParam, Vector3 secondParam)
    {
        first = firstParam;
        second = secondParam;
    }
}