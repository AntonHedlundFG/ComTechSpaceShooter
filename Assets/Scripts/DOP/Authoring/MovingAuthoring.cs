using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class MovingAuthoring : MonoBehaviour
{
    public Vector3 Velocity;
}

public class MovingBaker : Baker<MovingAuthoring>
{
    public override void Bake(MovingAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new MovingComponent
        {
            Velocity = authoring.Velocity
        });

        //This should do nothing if the entity already has a transform
        AddComponent(entity, new LocalTransform());
    }
}