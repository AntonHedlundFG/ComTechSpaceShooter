using Unity.Burst;
using Unity.Entities;

public readonly partial struct AsteroidAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<AsteroidComponentData> AsteroidData;

    public bool ShouldSplit() => AsteroidData.ValueRO.Tier >= 1;

}