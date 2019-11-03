using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TempPlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb;

    public Camera mainCamera;

    float moveSpeed;

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
        }       
    }
}
