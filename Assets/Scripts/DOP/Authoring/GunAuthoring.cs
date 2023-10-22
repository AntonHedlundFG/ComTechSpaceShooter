using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GunAuthoring : MonoBehaviour
{
    public float RateOfFire = 0.5f;
    public GameObject BulletPrefab;
    public float BulletSpeed = 25.0f;
    public float Damage = 1.0f;
    public float Duration = 7.0f;
    public int BulletCount = 5;
    public float BulletSpread = 3.0f;
}

public class GunBaker : Baker<GunAuthoring>
{
    public override void Bake(GunAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);

        AddComponent(entity, new GunComponent
        {
            rateOfFire = authoring.RateOfFire,
            bulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic),
            bulletSpeed = authoring.BulletSpeed,
            damage = authoring.Damage,
            duration = authoring.Duration,
            bulletCount = authoring.BulletCount,
            bulletSpread = authoring.BulletSpread,
            nextShootTime = 0.0f,
            random = new Unity.Mathematics.Random((uint)DateTime.Now.Ticks)
        });
    }
}