using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _velocity;
    private float _damage;
    
    void Update()
    {
        transform.position += _velocity * Time.deltaTime;
        
    }

    public void SetupBullet(Vector3 position, Vector3 velocity, float damage)
    {
        transform.position = position;
        _velocity = velocity;
        _damage = damage;
    }
}
