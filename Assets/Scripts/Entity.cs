using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] protected float angularSpeed = 360f;
    [SerializeField] private GameObject loot;
    private bool _isBusy;
    private float _lootSpawnRate = 0.3f;
    protected int CurrentHealth;
    public int id;

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        if(CurrentHealth > maxHealth)
            CurrentHealth = maxHealth;
    }

    public void ApplyDamage(int amount, GameObject deathExplosion)
    {
        //Debug.Log(_currentHealth + " - " + amount);
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Instantiate(deathExplosion, transform.position, transform.rotation).SetActive(true);
            if (Random.Range(0, 100) / 100f <= _lootSpawnRate && loot != null)
            {
                Instantiate(loot, transform.position, Quaternion.identity).SetActive(true);
            }
            Death();
        }
    }

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

    public float GetHealthRatio()
    {
        return CurrentHealth / (float)maxHealth;
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
