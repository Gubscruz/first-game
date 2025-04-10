using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float spawnInterval = 2f;
    public float incrementoIntervalo = 45f;
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
            lastIncrementoTime = Time.time;
        }
    }

    void SpawnMeteor()
    {
        if (mainCamera == null) return;
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        float randomX = Random.Range(bottomLeft.x, topRight.x);
        float randomY = Random.Range(bottomLeft.y, topRight.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 36f);
        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
        Meteor meteorScript = meteor.GetComponent<Meteor>();
        if (meteorScript != null)
            meteorScript.health = baseMeteorHealth;
    }
}