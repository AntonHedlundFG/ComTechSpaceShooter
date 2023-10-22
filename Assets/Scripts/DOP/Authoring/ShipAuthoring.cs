using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ShipAuthoring : MonoBehaviour
{
    public float MaxMoveSpeed = 25.0f;
    public float AccelerationRate = 50.0f;
    public float MovementDrag = 4.0f;
    public float MaxRotationSpeed = 200.0f;
    public float RotationAccelerationRate = 700.0f;
    public float RotationDrag = 5.0f;
}


public class ShipBaker : Baker<ShipAuthoring>
{
    public override void Bake(ShipAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        
        AddComponent(entity, new ShipComponent
        {
            MaxMoveSpeed = authoring.MaxMoveSpeed,
            AccelerationRate = authoring.AccelerationRate,
            MovementDrag = authoring.MovementDrag,
            MaxRotationSpeed = authoring.MaxRotationSpeed,
            RotationAccelerationRate = authoring.RotationAccelerationRate,
            RotationDrag = authoring.RotationDrag
        });

        AddComponent(entity, new MovingComponent { Velocity = Unity.Mathematics.float3.zero });
        AddComponent(entity, new RotationComponent { Value = 0.0f });
    }
}
