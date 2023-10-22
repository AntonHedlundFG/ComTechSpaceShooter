using Unity.Burst;
using Unity.Entities;

public partial struct MovingJob : IJobEntity
{
    public float DeltaTime;

    private void Execute(MovableAspect Movable)
    {
        Movable.Move(DeltaTime);
    }
}
