using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public struct ShipComponent : IComponentData
{
    public float MaxMoveSpeed;
    public float AccelerationRate;
    public float MovementDrag;
    public float MaxRotationSpeed;
    public float RotationAccelerationRate;
    public float RotationDrag;
}