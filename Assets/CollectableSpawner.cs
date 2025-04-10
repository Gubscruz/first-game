using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [Header("Collectible Settings")]
    public GameObject collectiblePrefab;
    public int initialSpawnCount = 5; // Number of collectibles generated at the beginning of the game.
    public float spawnInterval = 10f; // How frequently (in seconds) a new collectible is generated during gameplay.

    private float nextSpawnTime = 0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // Spawn the initial collectibles.
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnCollectible();
        }
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        // Continuously spawn new collectibles based on the spawn interval.
        if (Time.time >= nextSpawnTime)
        {
            SpawnCollectible();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnCollectible()
    {
        if (collectiblePrefab == null)
        {
            Debug.LogError("Collectible prefab is not assigned!");
            return;
        }
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }
        
        // Calculate the visible area using the camera's viewport.
        float zDistance = -mainCamera.transform.position.z;
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, zDistance));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, zDistance));

        float randomX = Random.Range(bottomLeft.x, topRight.x);
        float randomY = Random.Range(bottomLeft.y, topRight.y);

        // Assume collectibles are to be placed at z = 0.
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);
        Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);
    }
}