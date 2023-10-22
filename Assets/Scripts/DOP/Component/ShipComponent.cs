using Unity.Entities;

public struct ShipComponent : IComponentData
{
    public float MaxMoveSpeed;
    public float AccelerationRate;
    public float MovementDrag;
    public float MaxRotationSpeed;
    public float RotationAccelerationRate;
    public float RotationDrag;
}