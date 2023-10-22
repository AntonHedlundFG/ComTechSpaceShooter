using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

public readonly partial struct MovableAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<LocalTransform> LocalTransform;
    private readonly RefRO<MovingComponent> MovingComponentData;

    public void Move(float DeltaTime)
    {
        LocalTransform.ValueRW.Position += MovingComponentData.ValueRO.Velocity * DeltaTime;
    }
}