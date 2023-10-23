using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
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

            foreach (var gun in SystemAPI.Query<RefRW<GunComponent>>())
            {
                if (gun.ValueRO.nextShootTime < SystemAPI.Time.ElapsedTime)
                {
                    gun.ValueRW.nextShootTime = (float)SystemAPI.Time.ElapsedTime + gun.ValueRO.rateOfFire;

                    for (int i = 0; i < gun.ValueRO.bulletCount; i++)
                    {
                        var entity = ecb.Instantiate(gun.ValueRW.bulletPrefab);
                        float3 velocity = new float3(0, 0, 0);
                        float3 position = new float3(0, 0, 0);

                        //Not pretty solution, but I know only one ship exists. 
                        //I'm sure theres a way to get it as a singleton, but I'm moving on.
                        foreach (var ship in SystemAPI.Query<ShipAspect>())
                        {
                            position = ship.BulletSpawnLocation();
                            velocity = ship.ForwardVector();
                            break;
                        }

                        //Set bullet speed and add spread
                        velocity *= gun.ValueRO.bulletSpeed;
                        float rotateZ = gun.ValueRW.random.NextFloat(-gun.ValueRO.bulletSpread, gun.ValueRO.bulletSpread);
                        velocity = math.mul(quaternion.RotateZ(math.radians(rotateZ)), velocity);
                        
                        ecb.AddComponent(entity, new MovingComponent { Velocity = velocity });
                        ecb.AddComponent(entity, new LocalTransform { Position = position, Rotation = quaternion.identity, Scale = 1.0f });
                        ecb.AddComponent(entity, new DestroyedAfterDurationComponent { Duration = gun.ValueRO.duration, SpawnTime = (float)SystemAPI.Time.ElapsedTime});
                        ecb.AddComponent(entity, new BulletComponent());
                    }


                }
            }
        }
    }
}
