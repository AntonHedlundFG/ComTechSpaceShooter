using Unity.Burst;
using Unity.Entities;

public partial struct DestroyAfterDurationJob : IJobEntity
{
    public EntityCommandBuffer ECB;
    public float CurrentTime;

    private void Execute(Entity entity, DestroyedAfterDurationComponent DADComp)
    {
        if (CurrentTime >= DADComp.SpawnTime + DADComp.Duration)
        {
            ECB.DestroyEntity(entity);
        }
    }
}
