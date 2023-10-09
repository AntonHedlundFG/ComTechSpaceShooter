using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _velocity;
    private float _damage;
    private float _duration;

    private float _spawnTime;

    void Update()
    {
        transform.position += _velocity * Time.deltaTime;

        if (Time.time >= _spawnTime + _duration)
        {
            Destroy(gameObject);
        }
    }

    public void SetupBullet(Vector3 position, Vector3 velocity, float damage, float duration)
    {
        transform.position = position;
        _velocity = velocity;
        _damage = damage;
        _duration = duration;
        _spawnTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health;
        if (!other.gameObject.TryGetComponent<Health>(out health) || health.IsFriendly())
            return;

        health.DealDamage(_damage);
        Destroy(gameObject);
    }

}
