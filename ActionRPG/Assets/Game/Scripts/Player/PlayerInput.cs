using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerInput : MonoBehaviour
{
    public GameObject vectorObject;

    NavMeshAgent agent;
    public Animator anim;
    RaycastHit hitInfo = new RaycastHit();

    public GameObject spinObject;

    PlayerSpin playerSpin;

    float refSlideCd = 1f;
    float slideCd = 0;

    float baseClickTick = 0.2f;
    float clickTick;
    bool leftClickHeld = false;

    float refSpinCd = 2f;
    float spinCd = 0;

    float baseSpeed;
    float baseAcceleration;
    float slideSpeed;
    float slideAcceleration;
    float turnSpeed;

    public float slideAccelMultipier = 2f;

    Vector3 nextDestination = Vector3.zero;

    bool isSliding = false;
    bool isSpinning = false;
    bool firstAlpha3Press = true;

    Vector3 firstVectorPoint = Vector3.zero;

    public List<SliceVectors> sliceVectorList = new List<SliceVectors>(); 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        playerSpin = spinObject.GetComponent<PlayerSpin>();

        slideCd = refSlideCd;
        spinCd = refSpinCd;
        clickTick = 0;

        baseSpeed = agent.speed;
        baseAcceleration = agent.acceleration;
        turnSpeed = agent.angularSpeed;
        slideSpeed = baseSpeed * 1.5f;
        slideAcceleration = baseAcceleration * slideAccelMultipier;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftClickHeld = true;
        }
        if (clickTick < 0 && leftClickHeld)
        {
            OnLeftMoveClick();
            clickTick = baseClickTick;
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            leftClickHeld = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnRightClick();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (firstAlpha3Press)
            {
                OnKey3Pressed(true);
                firstAlpha3Press = false;
            }
            else
            {
                OnKey3Pressed(false);
                firstAlpha3Press = true;
            }
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

        clickTick -= Time.deltaTime;
    }

    void OnRightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
        {           
            firstVectorPoint = hitInfo.point;
        }
    }
    void OnKey3Pressed(bool firstPress)
    {
        if (firstPress)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                firstVectorPoint = hitInfo.point;
                MainVector vectorScript = vectorObject.GetComponent<MainVector>();
                vectorScript.TwoStepSlice(firstVectorPoint , Vector3.zero, true);
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                Vector3 secondVectorPoint = secondVectorPoint = hitInfo.point;
                MainVector vectorScript = vectorObject.GetComponent<MainVector>();
                vectorScript.TwoStepSlice(firstVectorPoint, secondVectorPoint, false);
                firstVectorPoint = Vector3.zero;
            }
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
        //Ray Ray;
        //RaycastHit[] hits = Physics.RaycastAll(firstPoint, secondPoint.normalized, Vector3.Distance(firstPoint, secondPoint));

        SliceVectors sliceVectors = new SliceVectors(firstPoint, secondPoint);
        sliceVectorList.Add(sliceVectors);

        MainVector vectorScript = vectorObject.GetComponent<MainVector>();
        vectorScript.StartSlice(sliceVectors.first, sliceVectors.second);
        
    }
    void OnLeftMoveClick()
    {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
         {
            if (!isSliding && !isSpinning)
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
        if (anim.GetBool("move"))
        {
            isSliding = true;
            anim.SetBool("slide", true);

            agent.speed = slideSpeed;
            agent.acceleration = slideAcceleration;
            agent.destination += transform.forward.normalized * 4;

            yield return new WaitForSeconds(0.8f);
            agent.ResetPath();
            anim.SetBool("slide", false);
            anim.SetBool("move", true);
            isSliding = false;

            if (nextDestination != Vector3.zero)
            {
                agent.destination = nextDestination;
            }
            agent.speed = baseSpeed;
            agent.acceleration = baseAcceleration;
            
        }
        
    }
    IEnumerator Spin()
    {
        isSpinning = true;
        Debug.Log("yaaaaaaaaaaaay");
        anim.SetBool("move", false);
        anim.SetBool("spin", true);
        agent.ResetPath();
        agent.isStopped = true;

        playerSpin.SpinActivated();
        
        yield return new WaitForSeconds(1.1f);

        playerSpin.SpinDeactivated();

        anim.SetBool("move", false);
        
        isSpinning = false;
        agent.isStopped = false;
        anim.SetBool("spin", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
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