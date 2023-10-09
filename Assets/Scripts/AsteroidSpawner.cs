using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private Border _border;
    [SerializeField] private float _asteroidSpeed = 5.0f;
    [SerializeField][Range(0.0f, 90.0f)] private float _maxRotationOffset = 35.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (_asteroidPrefab == null || _border == null)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SpawnAsteriod();
        }
    }

    private void SpawnAsteriod()
    {
        GameObject newObject = Instantiate(_asteroidPrefab);
        Asteroid newAsteroid = newObject.GetComponent<Asteroid>();
        if (newAsteroid == null)
        {
            Destroy(newObject);
            return;
        }
        
        //We use the border to determine a valid spawn point. We start by aiming the asteroids movement towards the center of the board
        //Then we randomly rotate its direction based on the maximum rotation offset.
        Vector3 spawnPoint = _border.GetRandomSpawnPoint();
        Vector3 spawnVelocity = -spawnPoint.normalized * _asteroidSpeed;
        spawnVelocity = Quaternion.Euler(new Vector3(0, 0, Random.Range(-_maxRotationOffset, _maxRotationOffset))) * spawnVelocity;
        
        newAsteroid.SetupAsteroid(spawnPoint, spawnVelocity, 10.0f);
    }
}
