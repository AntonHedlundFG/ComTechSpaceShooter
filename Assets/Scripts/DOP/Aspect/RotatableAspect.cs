using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public readonly partial struct RotatableAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<LocalTransform> LocalTransform;
    private readonly RefRW<RotationComponent> RotationComponent;

    [BurstCompile]
    public void Rotate(float DeltaTime)
    {
        quaternion previousRotation = LocalTransform.ValueRO.Rotation;

        quaternion newRotation = math.mul(quaternion.RotateZ(math.radians(RotationComponent.ValueRO.Value) * DeltaTime), previousRotation);

        LocalTransform.ValueRW.Rotation = newRotation;
    }
}