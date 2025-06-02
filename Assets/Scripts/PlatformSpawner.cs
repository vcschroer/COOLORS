using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform1Prefab;
    public GameObject platform2Prefab;
    public GameObject platform3Prefab;
    public GameObject platform5Prefab;
    public GameObject platform6Prefab;
    public Transform player;
    public int initialPlatforms = 5; 
    public float platformSpacing = 5f; 
    public float spawnOffset = 5f; 
    private List<GameObject> platforms = new List<GameObject>(); 
    private float nextSpawnPosition;
    private float timer = 0f;
    private GameObject currentPlatformPrefab; 

    void Start()
    {
        nextSpawnPosition = player.position.x + 5f;
        currentPlatformPrefab = platform1Prefab;
        platformSpacing = 5f;

        for (int i = 0; i < initialPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 75f)
        {
            currentPlatformPrefab = platform6Prefab;
            platformSpacing = 10f;
        }
        else if (timer >= 60f)
        {
            currentPlatformPrefab = platform5Prefab;
            platformSpacing = 10f;
        }
        else if (timer >= 45f)
        {
            currentPlatformPrefab = platform3Prefab;
            platformSpacing = 10f;
        }
        else if (timer >= 30f)
        {
            currentPlatformPrefab = platform3Prefab;
            platformSpacing = 8f;
        }
        else if (timer >= 15f)
        {
            currentPlatformPrefab = platform2Prefab;
            platformSpacing = 8f;
        }
        else
        {
            currentPlatformPrefab = platform1Prefab;
            platformSpacing = 5f;
        }

        if (player.position.x + spawnOffset > nextSpawnPosition)
        {
            SpawnPlatform();
            RemoveOldPlatforms();
        }
    }

    void SpawnPlatform()
    {
        float yOffset = Random.Range(-2f, 0.5f);
        Vector3 spawnPos = new Vector3(nextSpawnPosition, yOffset, 0); 
        GameObject newPlatform = Instantiate(currentPlatformPrefab, spawnPos, Quaternion.identity);
        platforms.Add(newPlatform);
        nextSpawnPosition += platformSpacing;
    }

    void RemoveOldPlatforms()
    {
        if (platforms.Count > initialPlatforms)
        {
            Destroy(platforms[0]);
            platforms.RemoveAt(0);
        }
    }
}
