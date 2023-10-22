using Unity.Burst;
using Unity.Entities;
using UnityEngine;

public partial struct MovingSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        new MovingJob { DeltaTime = Time.deltaTime }.Schedule();
    }
}