using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct AsteroidSpawnSystem : ISystem
{
    private EntityQuery _asteroidQuery;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        _asteroidQuery = state.GetEntityQuery(ComponentType.ReadOnly<AsteroidComponentData>());
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var spawn in SystemAPI.Query<RefRW<AsteroidSpawnerComponent>>())
        {
            //Check what our spawnrate is this frame, based on our rate per second.
            //Makes sure we don't spawn asteroids past our spawners targetAmount.
            int spawnAmount = (int)math.ceil(SystemAPI.Time.DeltaTime * spawn.ValueRO.spawnRate);
            int currentAsteroidCount = _asteroidQuery.CalculateEntityCount();
            spawnAmount = math.min(spawnAmount, spawn.ValueRO.targetAmount - currentAsteroidCount);

            for (int i = 0; i < spawnAmount; i++)
            {
                var entity = ecb.Instantiate(spawn.ValueRW.prefab);
                ecb.AddComponent(entity, new AsteroidComponentData { Tier = 2 });

                //Looks complicated, but simply chooses which side of the screen to spawn from
                //first, X or Y axis?
                //then, top/left or bottom/right?
                //then select random point along that side
                float3 position = new float3(0, 0, 0);
                bool lerpAlongX = spawn.ValueRW.random.NextBool();
                bool topLeft = spawn.ValueRW.random.NextBool();
                float lerpValue = spawn.ValueRW.random.NextFloat(0.0f, 1.0f);
                if (lerpAlongX)
                {
                    position.y = topLeft ? spawn.ValueRW.topLeft.y : spawn.ValueRW.btmRight.y;
                    position.x = spawn.ValueRW.topLeft.x * lerpValue + spawn.ValueRW.btmRight.x * (1.0f - lerpValue);
                }
                else
                {
                    position.x = topLeft ? spawn.ValueRW.topLeft.x : spawn.ValueRW.btmRight.x;
                    position.y = spawn.ValueRW.topLeft.y * lerpValue + spawn.ValueRW.btmRight.y * (1.0f - lerpValue);
                }
                float scale = 2.0f;
                ecb.AddComponent(entity, new LocalTransform { Position = position, Rotation = Quaternion.identity, Scale = scale });
                
                //Sets velocity to aim at the centre of the board
                //then randomly shift the aim slightly
                float3 velocity = math.normalize(-position);
                velocity *= spawn.ValueRW.speed;
                float rotateZ = spawn.ValueRW.random.NextFloat(-spawn.ValueRO.rotationOffset, spawn.ValueRO.rotationOffset);
                velocity = math.mul(quaternion.RotateZ(math.radians(rotateZ)), velocity);
                ecb.AddComponent(entity, new MovingComponent { Velocity = velocity });
            }
        }

    }
}
