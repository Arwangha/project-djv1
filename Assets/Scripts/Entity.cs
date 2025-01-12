using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float angularSpeed = 360f;
    private bool _isBusy;

    protected void RotateTowardsTarget(Vector3 target, float speed)
    {
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation(target),
            speed * Time.deltaTime);
        var angles = transform.rotation.eulerAngles;
        angles.x = 0;
        angles.z = 0;
        var rotation = transform.rotation;
        rotation.eulerAngles = angles;
        transform.rotation = rotation;
    }
}
