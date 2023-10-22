using Unity.Entities;
using Unity.Mathematics;

public struct MovingComponent : IComponentData
{
    public float3 Velocity;
}