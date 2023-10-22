using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(MovingSystem))]
public partial struct EdgeOfScreenSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        state.RequireForUpdate<AsteroidSpawnerComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        AsteroidSpawnerComponent spawnerComp = SystemAPI.GetSingleton<AsteroidSpawnerComponent>();

        var job = new EdgeOfScreenJob { spawnerComp = spawnerComp };
        job.Schedule();
    }
}
