using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct ShipInputSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var ship in SystemAPI.Query<ShipAspect>())
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                ship.Accelerate(SystemAPI.Time.DeltaTime);
            }

            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                ship.Rotate(SystemAPI.Time.DeltaTime, true);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                ship.Rotate(SystemAPI.Time.DeltaTime, false);
            }

            ship.ApplyDrag(SystemAPI.Time.DeltaTime);

        }

    }
}
