using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float spawnInterval = 2f;
    public float incrementoIntervalo = 45f;
    public float spawnRateIncreaseAmount = 0.1f;
    public float minSpawnInterval = 0.5f;
    private float nextSpawnTime = 0f;
    private float lastIncrementoTime = 0f;
    private int baseMeteorHealth = 1;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnMeteor();
            nextSpawnTime = Time.time + spawnInterval;
        }

        if (Time.time - lastIncrementoTime >= incrementoIntervalo)
        {
            baseMeteorHealth++;
            // Decrease spawn interval to increase spawn rate
            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - spawnRateIncreaseAmount);
            lastIncrementoTime = Time.time;
        }
    }

    void SpawnMeteor()
    {
        if (mainCamera == null) return;
        
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 bottomRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 0));
        Vector2 topLeft = mainCamera.ViewportToWorldPoint(new Vector2(0, 1));
        
        // Choose one of the four corners randomly
        Vector2 spawnPosition;
        int corner = Random.Range(0, 4);
        
        switch (corner)
        {
            case 0:
                spawnPosition = bottomLeft;
                break;
            case 1:
                spawnPosition = topLeft;
                break;
            case 2:
                spawnPosition = topRight;
                break;
            case 3:
                spawnPosition = bottomRight;
                break;
            default:
                spawnPosition = bottomLeft;
                break;
        }
        
        // Add a small offset from the exact corner
        float offset = 0.1f;
        if (corner == 0 || corner == 3) // bottom corners
            spawnPosition.y += offset;
        else // top corners
            spawnPosition.y -= offset;
            
        if (corner == 0 || corner == 1) // left corners
            spawnPosition.x += offset;
        else // right corners
            spawnPosition.x -= offset;
            
        Vector3 spawnPos3D = new Vector3(spawnPosition.x, spawnPosition.y, 36f);
        GameObject meteor = Instantiate(meteorPrefab, spawnPos3D, Quaternion.identity);
        Meteor meteorScript = meteor.GetComponent<Meteor>();
        if (meteorScript != null)
            meteorScript.health = baseMeteorHealth;
    }
}