using Unity.Burst;
using Unity.Entities;

public readonly partial struct AsteroidAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<AsteroidComponentData> DemoProps;

    public bool ShouldSplit() => DemoProps.ValueRO.Tier >= 1;

}