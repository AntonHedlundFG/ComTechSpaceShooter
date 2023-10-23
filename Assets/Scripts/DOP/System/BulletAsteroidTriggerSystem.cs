using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public partial struct BulletAsteroidTriggerSystem : ISystem
{
    private NativeHashSet<Entity> AsteroidDestroyList;

   [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        AsteroidDestroyList = new NativeHashSet<Entity>(100, Allocator.Persistent);
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        AsteroidDestroyList.Dispose();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        AsteroidDestroyList.Clear();
        var job = new BulletAsteroidTriggerJob
        {
            BulletGroup = SystemAPI.GetComponentLookup<BulletComponent>(),
            AsteroidGroup = SystemAPI.GetComponentLookup<AsteroidComponentData>(),
            ECB = ecb,
            AsteroidDestroyList = AsteroidDestroyList

        };
        state.Dependency = job.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
    }
}