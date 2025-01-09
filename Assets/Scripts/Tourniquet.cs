using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourniquet : MonoBehaviour
{
    [SerializeField] private GameObject passengerPrefab;
    [SerializeField] private float validationTime;
    private List<Passenger> _passengers = new List<Passenger>();
    private bool _busy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_busy) StartCoroutine(Validation());
    }

    private IEnumerator Validation()
    {
        foreach (var guy in _passengers)
        {
            guy.MoveForward();
        }
        yield return new WaitForSeconds(validationTime);
    }
}
