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
                count = query.ToEntityArray(Allocator.Temp).Length;
                query.Dispose();
            }
            else
            {
                count = AsteroidSpawner.AsteroidCount;
            }
            _text.text = "Asteroid Count: " + count;
        }
    }
}
