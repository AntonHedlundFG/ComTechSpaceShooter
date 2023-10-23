using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Collections;

public partial struct BulletAsteroidTriggerJob : ITriggerEventsJob
{
    [ReadOnly] public ComponentLookup<BulletComponent> BulletGroup;
    public ComponentLookup<AsteroidComponentData> AsteroidGroup;
    public EntityCommandBuffer ECB;
    public NativeHashSet<Entity> AsteroidDestroyList;

    public void Execute(TriggerEvent triggerEvent)
    {
        Entity A = triggerEvent.EntityA;
        Entity B = triggerEvent.EntityB;

        if (BulletGroup.HasComponent(A)
            && AsteroidGroup.HasComponent(B))
            HandleCollision(A, B);

        if (BulletGroup.HasComponent(B)
            && AsteroidGroup.HasComponent(A))
            HandleCollision(B, A);
    }

    private void HandleCollision(Entity Bullet, Entity Asteroid)
    {
        if (AsteroidDestroyList.Contains(Asteroid))
            return;
        ECB.DestroyEntity(Bullet);
        var asteroidComp = AsteroidGroup.GetRefRW(Asteroid);
        Debug.Log(asteroidComp.ValueRO.Tier);
        ECB.DestroyEntity(Asteroid);
        AsteroidDestroyList.Add(Asteroid);


    }

}
