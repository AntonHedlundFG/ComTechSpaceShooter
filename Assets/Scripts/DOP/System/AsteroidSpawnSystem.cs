using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct AsteroidSpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var spawn in SystemAPI.Query<RefRW<AsteroidSpawnerComponent>>())
        {

            int spawnAmount = (int)math.ceil(SystemAPI.Time.DeltaTime * spawn.ValueRO.spawnRate);

            for (int i = 0; i < spawnAmount; i++)
            {
                spawn.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawn.ValueRO.spawnRate;

                var entity = ecb.Instantiate(spawn.ValueRW.prefab);
                ecb.AddComponent(entity, new AsteroidComponentData { Tier = 2 });

                bool lerpAlongX = spawn.ValueRW.random.NextBool();
                bool topLeft = spawn.ValueRW.random.NextBool();
                float3 position = new float3(0, 0, 0);

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
                float3 velocity = math.normalize(-position);
                velocity *= spawn.ValueRW.speed;
                float rotateZ = spawn.ValueRW.random.NextFloat(-spawn.ValueRO.rotationOffset, spawn.ValueRO.rotationOffset);
                velocity = math.mul(quaternion.RotateZ(math.radians(rotateZ)), velocity);
                ecb.AddComponent(entity, new MovingComponent { Velocity = velocity });
            }

            /*
            if (SystemAPI.Time.ElapsedTime > spawn.ValueRW.nextSpawnTime)
            {
                
            }*/
        }

    }
}
