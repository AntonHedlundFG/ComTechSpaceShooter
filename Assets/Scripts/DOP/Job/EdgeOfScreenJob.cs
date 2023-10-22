using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct EdgeOfScreenJob : IJobEntity
{
    public AsteroidSpawnerComponent spawnerComp;

    private void Execute(MovableAspect Movable)
    {
        float3 position = Movable.LocalTransform.ValueRO.Position;
        float xDelta = spawnerComp.btmRight.x - spawnerComp.topLeft.x;
        float yDelta = spawnerComp.topLeft.y - spawnerComp.btmRight.y;
        if (position.x > spawnerComp.btmRight.x)
        {
            position.x -= xDelta;
        } else if (position.x < spawnerComp.topLeft.x)
        {
            position.x += xDelta;
        }

        if (position.y > spawnerComp.topLeft.y)
        {
            position.y -= yDelta;
        } else if (position.y < spawnerComp.btmRight.y)
        {
            position.y += yDelta;
        }

        Movable.LocalTransform.ValueRW.Position = position;
    }
}
