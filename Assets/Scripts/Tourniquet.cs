using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tourniquet : MonoBehaviour
{
    [SerializeField] private GameObject passengerPrefab;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float validationTime;
    [SerializeField] private float maxTargetX;
    [SerializeField] private float maxTargetZ;
    [SerializeField] private float minTargetX;
    [SerializeField] private float minTargetZ;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject alarm;
    private bool _upgraded;
    private List<Passenger> _passengers = new List<Passenger>();
    private bool _busy;
    void Start()
    {
        StartCoroutine(AddPassenger(3));
        StartCoroutine(UpdateDelay());
    }

    private IEnumerator UpdateDelay()
    {
        _busy = true;
        yield return new WaitForSeconds(3*validationTime);
        _busy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_busy) StartCoroutine(Validation());
    }

    private IEnumerator ShortAlarm()
    {
        alarm.SetActive(true);
        yield return new WaitForSeconds(validationTime);
        alarm.SetActive(false);
    }

    public void Upgrade()
    {
        _upgraded = true;
        door.SetActive(true);
    }

    private IEnumerator AddPassenger(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Passenger passenger = Instantiate(passengerPrefab, transform.position + offset, Quaternion.identity).GetComponent<Passenger>();
            passenger.transform.gameObject.SetActive(true);
            passenger.SetTarget( 
                new Vector3(Random.Range(minTargetX, maxTargetX), 
                transform.position.y, 
                Random.Range(minTargetZ, maxTargetZ)));
            _passengers.Add(passenger);
            if (_upgraded)
            {
                passenger.fraudeur = false;
            }
            MoveEveryOneForward();
            yield return new WaitForSeconds(validationTime);
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
        if (passenger.fraudeur)
        {
            StartCoroutine(ShortAlarm());
        }
        StartCoroutine(AddPassenger(1));
        _busy = false;
    }
}
