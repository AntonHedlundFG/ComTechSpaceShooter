using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;

[BurstCompile]
public struct MovingComponent : IComponentData
{
    public float3 Velocity;
}