using Unity.Burst;
using Unity.Entities;

public partial struct RotatingJob : IJobEntity
{
    public float DeltaTime;

    private void Execute(RotatableAspect Rotatable)
    {
        Rotatable.Rotate(DeltaTime);
    }
}
