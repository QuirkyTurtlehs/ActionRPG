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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerSpin = GetComponent<PlayerSpin>();

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
}
