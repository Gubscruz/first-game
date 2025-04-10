using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int damage = 1;
    
    private Rigidbody2D rb;
    private float spawnTime;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnTime = Time.time;
    }
    
    void Start()
    {
        // Make sure this projectile gets destroyed after its lifetime
        Destroy(gameObject, lifetime);
    }
    
    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
        
        // Rotate projectile to face the direction it's moving
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        // Log that the projectile was launched with its ID for debugging
        Debug.Log("Projectile " + GetInstanceID() + " launched with velocity " + rb.linearVelocity + " at time " + Time.time);
    }
    
    void Update()
    {
        // Double-check that the projectile is moving
        if (rb.linearVelocity.magnitude < 0.1f)
        {
            Debug.LogWarning("Projectile " + GetInstanceID() + " has stopped moving. Reapplying velocity.");
            rb.linearVelocity = transform.up * speed;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we hit an enemy
        if (collision.CompareTag("Enemy"))
        {
            // Get the Enemy health component and damage it
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            
            // Destroy the projectile
            Destroy(gameObject);
        }
    }
} 