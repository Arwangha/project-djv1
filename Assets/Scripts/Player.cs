using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : Entity
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float shotDelay;
    //[SerializeField] private Animator animator;

    private Camera _mainCamera;
    private CharacterController _characterController;
    private static readonly int PlayerId = 0;

    public static PlayerCharacter Instance;

    private int _runBoolHash;
    private int _shotTrigHash;
    
    protected void Awake()
    {
        _mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        id = PlayerId;
        //_runBoolHash = Animator.StringToHash("IsMoving");
        //_shotTrigHash = Animator.StringToHash("Shoot");
        Instance = this;
    }

    protected void Update()
    {
        //Debug.Log(CurrentHealth);
        if (Input.GetMouseButton(0))
        {
            //animator.SetBool(_runBoolHash, true);
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            var plane = new Plane(Vector3.up, Vector3.zero);
            if (plane.Raycast(ray, out var x))
            {
                var targetPosition = ray.GetPoint(x);
                var position = transform.position;

                var directionToTarget = targetPosition - position;

                var dot = Vector3.Dot(transform.forward, directionToTarget.normalized);
                var speedPenalty = (dot + 1f) / 2f;

                var newPosition = Vector3.MoveTowards(position, targetPosition, speedPenalty * speed * Time.deltaTime);
                _characterController.Move(newPosition - position);

                RotateTowardsTarget(directionToTarget, angularSpeed);
            }
        }

        else
        {
            //animator.SetBool(_runBoolHash, false);
        }
    }
}