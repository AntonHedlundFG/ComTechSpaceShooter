using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial struct MovingJob : IJobEntity
{
    public float DeltaTime;

    [BurstCompile]
    private void Execute(MovableAspect Movable)
    {
        Movable.Move(DeltaTime);
    }
}
