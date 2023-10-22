using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public struct AsteroidSpawnerComponent : IComponentData
{
    public Entity prefab;
    public float spawnRate;
    public float nextSpawnTime;
    public Random random;
    public float3 topLeft;
    public float3 btmRight;
    public float speed;
    public float rotationOffset;
}
