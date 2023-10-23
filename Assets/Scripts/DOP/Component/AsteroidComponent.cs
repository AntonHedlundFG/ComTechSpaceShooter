using Unity.Entities;
using Unity.Burst;

[BurstCompile]
public struct AsteroidComponentData : IComponentData
{
    public int Tier;
}