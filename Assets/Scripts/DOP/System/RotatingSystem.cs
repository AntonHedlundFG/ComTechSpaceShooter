using Unity.Burst;
using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(DestroyAfterDurationSystem))]
public partial struct RotatingSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var job = new RotatingJob { DeltaTime = Time.deltaTime };
        job.Schedule();
    }
}