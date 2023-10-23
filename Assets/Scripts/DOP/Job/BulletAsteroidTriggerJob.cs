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

    //We use this hashset to keep track of which asteroids have already been marked
    //for deletion, otherwise two bullets could collide with the same asteroid in one frame
    //and both bullets would be destroyed.
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
        ECB.DestroyEntity(Asteroid);
        AsteroidDestroyList.Add(Asteroid);
    }
}
