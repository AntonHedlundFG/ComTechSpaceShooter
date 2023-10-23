using Unity.Burst;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

public class AsteroidAuthoring : MonoBehaviour
{
    public int Tier;
}

public class AsteroidBaker : Baker<AsteroidAuthoring>
{
    public override void Bake(AsteroidAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        
        AddComponent(entity, new AsteroidComponentData
        {
            Tier = authoring.Tier
        });
    }
}