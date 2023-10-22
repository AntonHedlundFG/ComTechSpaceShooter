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
        /*
        var renderMeshArray = new RenderMeshArray() { Materials = authoring.MeshRenderer.sharedMaterials, Meshes = new Mesh[] { authoring.MeshFilter.sharedMesh } };
        var renderMeshDescription = new RenderMeshDescription() { FilterSettings = Unity.Entities.Graphics.RenderFilterSettings.Default, LightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off };
        RenderMeshUtility.AddComponents(entity, World.DefaultGameObjectInjectionWorld.EntityManager, renderMeshDescription, renderMeshArray);
        */
        AddComponent(entity, new AsteroidComponentData
        {
            Tier = authoring.Tier
        });
    }
}