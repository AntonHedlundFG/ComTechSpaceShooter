using Unity.Entities;
using Unity.Burst;

[BurstCompile]
public struct DestroyedAfterDurationComponent : IComponentData
{
    public float Duration;
    public float SpawnTime;
}