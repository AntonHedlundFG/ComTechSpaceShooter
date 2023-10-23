using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public readonly partial struct ShipAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<LocalTransform> LocalTransform;
    private readonly RefRW<ShipComponent> ShipComponent;
    private readonly RefRW<MovingComponent> MoveComponent;
    private readonly RefRW<RotationComponent> RotateComponent;

    [BurstCompile]
    public void Accelerate(float DeltaTime)
    {
        MoveComponent.ValueRW.Velocity += (ShipComponent.ValueRO.AccelerationRate * DeltaTime) * ForwardVector();
        if (math.length(MoveComponent.ValueRO.Velocity) > ShipComponent.ValueRO.MaxMoveSpeed)
        {
            MoveComponent.ValueRW.Velocity = math.normalize(MoveComponent.ValueRO.Velocity) * ShipComponent.ValueRO.MaxMoveSpeed;
        }
    }

    [BurstCompile]
    public void Rotate(float DeltaTime, bool ToLeft)
    {
        RotateComponent.ValueRW.Value += DeltaTime * ShipComponent.ValueRO.RotationAccelerationRate * (float)(ToLeft ? 1.0f : -1.0f);
        RotateComponent.ValueRW.Value = math.clamp(RotateComponent.ValueRO.Value, -ShipComponent.ValueRO.MaxRotationSpeed, ShipComponent.ValueRO.MaxRotationSpeed);
    }

    [BurstCompile]
    public void ApplyDrag(float DeltaTime)
    {
        MoveComponent.ValueRW.Velocity *= (1.0f - ShipComponent.ValueRO.MovementDrag * DeltaTime);
        RotateComponent.ValueRW.Value *= (1.0f - ShipComponent.ValueRO.RotationDrag * DeltaTime);
    }

    [BurstCompile]
    public float3 ForwardVector() => LocalTransform.ValueRO.Right();
    [BurstCompile] 
    public float3 BulletSpawnLocation() => LocalTransform.ValueRO.Position;
}