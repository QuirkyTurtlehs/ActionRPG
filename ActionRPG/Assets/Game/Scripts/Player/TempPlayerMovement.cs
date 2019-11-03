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
        //rb = GetComponent<Rigidbody>();
    }   
    void Update()
    {
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
            animat.SetFloat(speedHash, 1f);
            agent.SetDestination(hit.point);
        }       

    }
}
