using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;

[BurstCompile]
public struct RotationComponent : IComponentData
{
    public float Value;
}