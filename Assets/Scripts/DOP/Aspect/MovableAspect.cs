using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
public readonly partial struct MovableAspect : IAspect
{
    private readonly Entity entity;
    public readonly RefRW<LocalTransform> LocalTransform;
    private readonly RefRO<MovingComponent> MovingComponentData;

    [BurstCompile]
    public void Move(float DeltaTime)
    {
        LocalTransform.ValueRW.Position += MovingComponentData.ValueRO.Velocity * DeltaTime;
    }
}