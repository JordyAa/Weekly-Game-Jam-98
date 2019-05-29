using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private int maxSpawned = 4;
    [HideInInspector] public int spawned;
    
    [SerializeField] private float timeBetweenSpawns = 1f;
    private float timeUntilSpawn;
    
    [SerializeField] private int maxAsteroids = 2;
    [HideInInspector] public int asteroids;
    
    [SerializeField] private float timeBetweenAsteroids = 1f;
    private float timeUntilAsteroid;
    
    [SerializeField] private Transform[] spawnpoints = new Transform[0];
    
    [SerializeField] private GameObject[] enemies = new GameObject[0];
    [SerializeField] private GameObject asteroid = null;
    
    private Dragon player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Dragon>();
    }

    private void Update()
    {
        if (player.isDead) return;
        
        SpawnDragon();
        SpawnAsteroid();
    }

    private void SpawnDragon()
    {
        if (timeUntilSpawn > 0f)
        {
            timeUntilSpawn -= Time.deltaTime;
        }
        else if (spawned < maxSpawned)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)],
                spawnpoints[Random.Range(0, spawnpoints.Length)].position,
                Quaternion.identity);
            
            timeUntilSpawn = timeBetweenSpawns;
            spawned++;
        }
    }

    private void SpawnAsteroid()
    {
        if (timeUntilAsteroid > 0f)
        {
            timeUntilAsteroid -= Time.deltaTime;
        }
        else if (asteroids < maxAsteroids)
        {
            Instantiate(asteroid,
                spawnpoints[Random.Range(0, spawnpoints.Length)].position,
                Quaternion.identity);
            
            timeUntilAsteroid = timeBetweenAsteroids;
            spawned++;
        }
    }
}
