using Unity.Burst;
using Unity.Entities;
using UnityEngine;

public partial struct MovingSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var job = new MovingJob { DeltaTime = Time.deltaTime };
        job.Schedule();
    }
}