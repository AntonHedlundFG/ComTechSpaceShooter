using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;

[BurstCompile]
public class AsteroidSpawnerAuthoring : MonoBehaviour
{
    public GameObject AsteroidGameObject;
    public Transform TopLeftLocation;
    public Transform BtmRightLocation;
    public float SpawnRate = 5.0f;
    public float Speed = 1.0f;
    public float RotationOffset = 35.0f;
    public int TargetAmount = 10000;
}

[BurstCompile]
public class AsteroidSpawnerBaker : Baker<AsteroidSpawnerAuthoring>
{
    public override void Bake(AsteroidSpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new AsteroidSpawnerComponent
        {
            prefab = GetEntity(authoring.AsteroidGameObject, TransformUsageFlags.Dynamic),
            spawnRate = authoring.SpawnRate,
            random = new Unity.Mathematics.Random((uint)DateTime.Now.Ticks),
            topLeft = authoring.TopLeftLocation.position,
            btmRight = authoring.BtmRightLocation.position,
            speed = authoring.Speed,
            rotationOffset = authoring.RotationOffset,
            targetAmount = authoring.TargetAmount
        });
    }
}