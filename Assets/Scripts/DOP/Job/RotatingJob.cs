using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial struct RotatingJob : IJobEntity
{
    public float DeltaTime;

    [BurstCompile]
    private void Execute(RotatableAspect Rotatable)
    {
        Rotatable.Rotate(DeltaTime);
    }
}
