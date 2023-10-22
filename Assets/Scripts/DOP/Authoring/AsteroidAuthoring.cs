using Unity.Burst;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

//[RequireComponent(typeof(MeshRenderer))]
//[RequireComponent(typeof(MeshFilter))]
public class AsteroidAuthoring : MonoBehaviour
{
    public int Tier;
    //public MeshRenderer MeshRenderer;
    //public MeshFilter MeshFilter;
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