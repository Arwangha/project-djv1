using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Passenger : Entity
{
    public bool fraudeur;
    public float fraudLikelihood;
    public bool hasPassedGate;
    public Vector3 target;
    private NavMeshAgent _navMeshAgent;
    private bool _busy;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        fraudeur = Random.value > fraudLikelihood;
        target = transform.position;
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    public bool Control()
    {
        return fraudeur;
    }

    public IEnumerator MoveForward()
    {
        _busy = true;
        _navMeshAgent.destination = transform.position + transform.forward;
        yield return new WaitForSeconds(1f);
        _busy = false;
    }

    private void Update()
    {
        if (hasPassedGate)
        {
            _navMeshAgent.destination = target;
        }
        else if(!_busy)
        {
            _navMeshAgent.destination = transform.position;
        }
    }
}
