using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tourniquet : MonoBehaviour
{
    [SerializeField] private GameObject passengerPrefab;
    [SerializeField] private float validationTime;
    [SerializeField] private float maxTargetX;
    [SerializeField] private float maxTargetZ;
    [SerializeField] private float minTargetX;
    [SerializeField] private float minTargetZ;
    private List<Passenger> _passengers = new List<Passenger>();
    private bool _busy;
    void Start()
    {
        AddPassenger(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_busy) StartCoroutine(Validation());
    }

    private void AddPassenger(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Passenger passenger = Instantiate(passengerPrefab, transform.position, Quaternion.identity).GetComponent<Passenger>();
            passenger.transform.gameObject.SetActive(true);
            passenger.SetTarget( 
                new Vector3(Random.Range(minTargetX, maxTargetX), 
                transform.position.y, 
                Random.Range(minTargetZ, maxTargetZ)));
            _passengers.Add(passenger);
            MoveEveryOneForward();
        }
    }

    private void MoveEveryOneForward()
    {
        foreach (var guy in _passengers)
        {
            StartCoroutine(guy.MoveForward());
        }
    }

    private IEnumerator Validation()
    {
        _busy = true;
        yield return new WaitForSeconds(validationTime);
        Passenger passenger = _passengers.ElementAt(0);
        passenger.hasPassedGate = true;
        _passengers.Remove(passenger);
        AddPassenger(1);
        _busy = false;
    }
}
