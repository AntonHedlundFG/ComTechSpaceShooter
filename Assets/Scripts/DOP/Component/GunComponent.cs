using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;
using Unity.Burst;

[BurstCompile]
public struct GunComponent : IComponentData
{
    public float rateOfFire;
    public Entity bulletPrefab;
    public float bulletSpeed;
    public float damage;
    public float duration;
    public int bulletCount;
    public float bulletSpread;
    public float nextShootTime;
    public Random random;
}