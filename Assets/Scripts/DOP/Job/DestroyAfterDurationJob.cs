using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial struct DestroyAfterDurationJob : IJobEntity
{
    public EntityCommandBuffer ECB;
    public float CurrentTime;

    [BurstCompile]
    private void Execute(Entity entity, DestroyedAfterDurationComponent DADComp)
    {
        if (CurrentTime >= DADComp.SpawnTime + DADComp.Duration)
        {
            ECB.DestroyEntity(entity);
        }
    }
}
