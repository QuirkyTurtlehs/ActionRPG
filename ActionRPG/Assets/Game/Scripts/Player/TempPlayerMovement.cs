using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TempPlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb;
    public Animator animat;

    public Camera mainCamera;

    float moveSpeed;

    int speedHash = Animator.StringToHash("Speed");
    int directionHash = Animator.StringToHash("Direction");

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updatePosition = false;
        //rb = GetComponent<Rigidbody>();
    }   
    void Update()
    {
        Debug.Log(agent.velocity.magnitude);
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDown();
        }
        if (agent.velocity.magnitude < 0.1f)
        {
            animat.SetFloat(speedHash, 0.0f);
        }

    }
    void OnMouseButtonDown()
    {
        MovePlayer();
    }       
    void MovePlayer()
    {
        RaycastHit hit;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {          
            agent.SetDestination(hit.point);
            animat.SetFloat(speedHash, 2f);
        }       

    }
}
