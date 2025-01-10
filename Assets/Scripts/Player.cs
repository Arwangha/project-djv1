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
        scoreText.text = "Score : " + _score + "\nPoints d'amélioration : " + GameManager.UpgradePoints.ToString();
        Debug.Log(GameManager.UpgradePoints);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            //Debug.Log("layer6");
            if (other.gameObject.TryGetComponent(out Passenger passenger))
            {
                if (!passenger.fraudeur) return;
                passenger.fraudeur = false;
                passenger.EndControl();
                _score++;
                LevelManager.Instance.caughtFraudeurs++;
                scoreText.text = "Score : " + _score + "\nPoints d'amélioration : " + GameManager.UpgradePoints.ToString();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (other.gameObject.TryGetComponent(out Passenger passenger))
            {
                passenger.EndControl();
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
                    passenger.Control();
                }
                
                else if (hit.collider.gameObject.layer == 7)
                {
                    Tourniquet tourniquet = hit.collider.gameObject.GetComponent<Tourniquet>();
                    if (tourniquet.Upgrade())
                    {
                        scoreText.text = "Score : " + _score + "\nPoints d'amélioration : " + GameManager.UpgradePoints.ToString();
                    }
                }
            }
        }

        var position = transform.position;

        var directionToTarget = _movementTarget - position;

        var newPosition = Vector3.MoveTowards(position, _movementTarget, speed * Time.deltaTime);
        _characterController.Move(newPosition - position);

        RotateTowardsTarget(directionToTarget, angularSpeed);

        if (!LevelManager.Instance.canAct)
        {
            scoreText.text = "Score : " + _score + "\nPoints d'amélioration : " + GameManager.UpgradePoints.ToString();
        }
    }
}