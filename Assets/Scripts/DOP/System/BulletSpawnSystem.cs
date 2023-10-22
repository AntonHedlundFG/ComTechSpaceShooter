using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct BulletSpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        state.RequireForUpdate<GunComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var spawn in SystemAPI.Query<RefRW<GunComponent>>())
            {
                if (spawn.ValueRO.nextShootTime < SystemAPI.Time.ElapsedTime)
                {
                    spawn.ValueRW.nextShootTime = (float)SystemAPI.Time.ElapsedTime + spawn.ValueRO.rateOfFire;

                    for (int i = 0; i < spawn.ValueRO.bulletCount; i++)
                    {
                        var entity = ecb.Instantiate(spawn.ValueRW.bulletPrefab);
                        float3 velocity = new float3(1, 0, 0);
                        float3 position = new float3(0, 0, 0);

                        foreach (var ship in SystemAPI.Query<ShipAspect>())
                        {
                            position = ship.BulletSpawnLocation();
                            velocity = ship.ForwardVector();
                            break;
                        }

                        velocity *= spawn.ValueRO.bulletSpeed;
                        float rotateZ = spawn.ValueRW.random.NextFloat(-spawn.ValueRO.bulletSpread, spawn.ValueRO.bulletSpread);
                        velocity = math.mul(quaternion.RotateZ(math.radians(rotateZ)), velocity);
                        ecb.AddComponent(entity, new MovingComponent { Velocity = velocity });
                        ecb.AddComponent(entity, new LocalTransform { Position = position, Rotation = quaternion.identity, Scale = 1.0f });
                        ecb.AddComponent(entity, new DestroyedAfterDurationComponent { Duration = spawn.ValueRO.duration, SpawnTime = (float)SystemAPI.Time.ElapsedTime});
                    }


                }
            }
        }
    }
}
