using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Entities;
using Unity.Collections;

public class AsteroidCountUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private bool _dataOriented = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_text != null)
        {
            int count;
            if (_dataOriented)
            {
                var em = World.DefaultGameObjectInjectionWorld.EntityManager;
                var query = em.CreateEntityQuery(typeof(AsteroidComponentData));
                Entity[] entitiesArray = query.ToEntityArray(Allocator.Temp).ToArray();
                count = entitiesArray.Length;
                query.Dispose();
                /*
                var qb = SystemAPI.QueryBuilder();
                qb.WithAll<AsteroidComponentData>();
                EntityQuery query = qb.Build();
                count = query.CalculateEntityCount();
                query.Dispose();
                */
            }
            else
            {
                count = AsteroidSpawner.AsteroidCount;
            }
            _text.text = "Asteroid Count: " + count;
        }
    }
}
