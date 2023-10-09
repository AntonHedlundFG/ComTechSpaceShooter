using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Asteroid : MonoBehaviour
{
    private Vector3 _velocity;
    private float _damage = 5.0f;

    private Health _health;

    private void OnEnable()
    {
        _health = GetComponent<Health>();
        _health.onDeath.AddListener(BreakAsteroid);
    }

    private void OnDisable()
    {
        _health.onDeath.RemoveListener(BreakAsteroid);
    }

    void Update()
    {
        transform.position += _velocity * Time.deltaTime;
    }

    public void SetupAsteroid(Vector3 position, Vector3 velocity, float damage)
    {
        transform.position = position;
        _velocity = velocity;
        _damage = damage;
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
        Destroy(gameObject);
    }

}
