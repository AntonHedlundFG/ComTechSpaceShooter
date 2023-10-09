using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private Transform _upperRightCorner;

    // Start is called before the first frame update
    void Start()
    {
        if (_asteroidPrefab == null || _upperRightCorner == null)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
