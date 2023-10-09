using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] [Range(0.1f, 2.0f)] private float _shootCooldown = 0.5f;
    [SerializeField] [Range(1.0f, 50.0f)] private float _bulletSpeed = 10.0f;
    [SerializeField] [Range(1.0f, 10.0f)] private float _damage = 1.0f;
    [SerializeField] [Range(5.0f, 20.0f)] private float _duration = 10.0f;


    private float _lastFireTime;

    private void Start()
    {
        _lastFireTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= _lastFireTime + _shootCooldown)
        {
            _lastFireTime = Time.time;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_bulletPrefab == null) return;

        GameObject _newObject = Instantiate(_bulletPrefab);
        Bullet _newBullet = _newObject.GetComponent<Bullet>();
        if (_newBullet == null)
        {
            Destroy(_newObject);
            return;
        }
        _newBullet.SetupBullet(transform.position, transform.right * _bulletSpeed, _damage, _duration);

    }
}
