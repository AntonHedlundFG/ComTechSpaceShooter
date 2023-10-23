using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public readonly partial struct AsteroidAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<AsteroidComponentData> AsteroidData;

    [BurstCompile]
    public bool ShouldSplit() => AsteroidData.ValueRO.Tier >= 1;

}