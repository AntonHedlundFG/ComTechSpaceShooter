using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Asteroid : MonoBehaviour
{
    private Vector3 _velocity;
    private float _damage = 5.0f;
    private int _tier;

    private Health _health;
    private AsteroidSpawner _spawner;

    private void OnEnable()
    {
        _health = GetComponent<Health>();
        _health.onDeath.AddListener(BreakAsteroid);
        AsteroidSpawner.AsteroidCount++;
    }

    private void OnDisable()
    {
        _health.onDeath.RemoveListener(BreakAsteroid);
        AsteroidSpawner.AsteroidCount--;
    }

    void Update()
    {
        transform.position += _velocity * Time.deltaTime;
    }

    public void SetupAsteroid(AsteroidSpawner spawner, Vector3 position, Vector3 velocity, float damage, int Tier = 0)
    {
        _spawner = spawner;
        transform.position = position;
        _velocity = velocity;
        _damage = damage;
        _tier = Tier;
        _health.ResetHealth(5.0f * (Tier + 1));
        transform.localScale = Vector3.one * (Tier + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.gameObject.GetComponentInParent<Health>();
        if (health == null || !health.IsFriendly())
            return;

        health.DealDamage(_damage);
        _health.AutoKill();
    }

    private void BreakAsteroid()
    {
        _spawner.SplitAsteroid(this, _tier);
    }

    public float GetVelocity() { return _velocity.magnitude; }

}
