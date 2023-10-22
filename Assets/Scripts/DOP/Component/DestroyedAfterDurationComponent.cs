using Unity.Entities;

public struct DestroyedAfterDurationComponent : IComponentData
{
    public float Duration;
    public float SpawnTime;
}