using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public class MovingAuthoring : MonoBehaviour
{
    public Vector3 Velocity;
}

[BurstCompile]
public class MovingBaker : Baker<MovingAuthoring>
{
    public override void Bake(MovingAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new MovingComponent
        {
            Velocity = authoring.Velocity
        });

    }
}