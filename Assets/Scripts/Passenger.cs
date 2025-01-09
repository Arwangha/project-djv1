using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Passenger : Entity
{
    public bool fraudeur;
    public bool hasPassedGate;
    public Vector3 target;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public bool Control()
    {
        return fraudeur;
    }

    public void MoveForward()
    {
        _navMeshAgent.destination = transform.position + transform.forward;
    }

    private void Update()
    {
        if (hasPassedGate)
        {
            _navMeshAgent.destination = target;
        }
    }
}
