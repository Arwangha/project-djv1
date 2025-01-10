using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : Entity
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private TMP_Text scoreText;

    private int _score;
    //[SerializeField] private Animator animator;

    private Camera _mainCamera;
    private CharacterController _characterController;
    private static readonly int PlayerId = 0;

    public static PlayerCharacter Instance;

    private int _runBoolHash;
    private int _shotTrigHash;
    private Vector3 _movementTarget;
    
    protected void Awake()
    {
        _mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        id = PlayerId;
        //_runBoolHash = Animator.StringToHash("IsMoving");
        //_shotTrigHash = Animator.StringToHash("Shoot");
        Instance = this;
        _movementTarget = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 6)
        {
            if (other.gameObject.TryGetComponent<Passenger>(out Passenger passenger))
            {
                if (!passenger.fraudeur) return;
                passenger.fraudeur = false;
                passenger.EndControl();
                _score++;
                scoreText.text = "Score : " + _score.ToString();
            }
        }
    }

    protected void Update()
    {
        //Debug.Log(CurrentHealth);
        if (Input.GetMouseButton(0))
        {
            //animator.SetBool(_runBoolHash, true);
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            var plane = new Plane(Vector3.up, Vector3.zero);
            RaycastHit hit;
            if (plane.Raycast(ray, out var x))
            {
                _movementTarget = ray.GetPoint(x);
                
            }

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == 6)
                {
                    Passenger passenger = hit.collider.gameObject.GetComponent<Passenger>();
                    if (passenger.Control())
                    {

                    }
                }
            }
        }

        var position = transform.position;

        var directionToTarget = _movementTarget - position;

        var newPosition = Vector3.MoveTowards(position, _movementTarget, speed * Time.deltaTime);
        _characterController.Move(newPosition - position);

        RotateTowardsTarget(directionToTarget, angularSpeed);

    }
}