using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int scoreValue = 1;
    public bool autoAttractToPlayer = true;
    public float attractSpeed = 5f;
    public float attractDistance = 3f;
    
    private Transform playerTransform;
    
    void Start()
    {
        // Ensure the collectable has the correct tag
        if (gameObject.tag != "Coletavel")
        {
            gameObject.tag = "Coletavel";
        }
        
        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }
    
    void Update()
    {
        
        // Optional: Attract to player when close
        if (autoAttractToPlayer && playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= attractDistance)
            {
                // Stronger attraction the closer the player is
                float attractFactor = 1f - (distanceToPlayer / attractDistance);
                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    playerTransform.position, 
                    attractSpeed * attractFactor * Time.deltaTime
                );
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Increase score
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
            }
            
            // Destroy the collectable
            Destroy(gameObject);
        }
    }
} 