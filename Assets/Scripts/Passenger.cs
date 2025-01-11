using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Passenger : Entity
{
    public bool fraudeur;
    public float fraudLikelihood;
    public bool hasPassedGate;
    public Vector3 target;
    private bool beingControlled;
    private NavMeshAgent _navMeshAgent;
    private bool _busy;
        
    private Animator _animator;


    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        fraudeur = (Random.value < fraudLikelihood);
        target = transform.position;
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    public bool Control()
    {
        beingControlled = true;
        return fraudeur;
    }

    public void EndControl()
    {
        beingControlled = false;
    }
    
    
    public IEnumerator Fraud()
    {
        _animator.enabled = true;
        yield return new WaitForSeconds(1f);
        _animator.enabled = false;
    }

    public IEnumerator MoveForward()
    {
        _busy = true;
        _navMeshAgent.destination = transform.position + 5*transform.forward;
        yield return new WaitForSeconds(1f);
        _busy = false;
    }

    private void Update()
    {
        if (hasPassedGate && !beingControlled)
        {
            _navMeshAgent.destination = target;
        }
        else if(!_busy || beingControlled)
        {
            _navMeshAgent.destination = transform.position;
        }
    }
}
