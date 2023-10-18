using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private Border _border;
    [SerializeField] private float _asteroidSpeed = 5.0f;
    [SerializeField] [Range(0.0f, 90.0f)] private float _maxRotationOffset = 35.0f;
    [SerializeField] private float _spawnRate = 5.0f;
    private float _lastSpawnTime;

    public static int AsteroidCount;
    [SerializeField] [Range(0, 1000000)] private int _targetAsteroidCount;
    [SerializeField] [Range(0, 1000000)] private int _maxAsteroidsPerSpawnCycle;

    // Start is called before the first frame update
    void Start()
    {
        if (_asteroidPrefab == null || _border == null)
            Destroy(gameObject);

        _lastSpawnTime = Time.time;
        AsteroidCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _lastSpawnTime + _spawnRate)
        {
            SpawnAsteroidRandomly(-1);
            _lastSpawnTime = Time.time;
        }


    }

    private void SpawnAsteroidRandomly(int tier = 0)
    {
        if (tier == -1)
        {
            tier = Random.Range(0, 4);
        }
        //We use the border to determine a valid spawn point. We start by aiming the asteroids movement towards the center of the board
        //Then we randomly rotate its direction based on the maximum rotation offset.

        int spawnAmount = Mathf.Min(_maxAsteroidsPerSpawnCycle, _targetAsteroidCount - AsteroidCount);

        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnPoint = _border.GetRandomSpawnPoint();
            Vector3 spawnVelocity = -spawnPoint.normalized * _asteroidSpeed;
            spawnVelocity = Quaternion.Euler(new Vector3(0, 0, Random.Range(-_maxRotationOffset, _maxRotationOffset))) * spawnVelocity;
            SpawnAsteroidAt(spawnPoint, spawnVelocity, tier);
        }
    }

    private void SpawnAsteroidAt(Vector3 location, Vector3 direction, int tier = 0)
    {
        GameObject newObject = Instantiate(_asteroidPrefab);
        Asteroid newAsteroid = newObject.GetComponent<Asteroid>();
        if (newAsteroid == null)
        {
            Destroy(newObject);
            return;
        }

        newAsteroid.SetupAsteroid(this, location, direction, 10.0f, tier);
    }

    public void SplitAsteroid(Asteroid asteroid, int currentTier)
    {
        if (currentTier > 0)
        {
            Vector3 randomDir = Quaternion.Euler(new Vector3(0, 0, Random.Range(0.0f, 360.0f))) * Vector3.right;
            SpawnAsteroidAt(asteroid.transform.position, randomDir * asteroid.GetVelocity() * 1.25f, currentTier - 1);
            randomDir = Quaternion.Euler(new Vector3(0, 0, Random.Range(0.0f, 360.0f))) * Vector3.right;
            SpawnAsteroidAt(asteroid.transform.position, randomDir * asteroid.GetVelocity() * 1.25f, currentTier - 1);
        }

        Destroy(asteroid.gameObject);
    }
}
