using UnityEngine;

public class Meteor : MonoBehaviour
{
    public int health = 1;
    public int damageToPlayer = 1;
    public float speed = 2f;

    private Transform playerTransform;

    void Start()
    {
        // Make sure this object has the Meteor tag
        if (gameObject.tag != "Meteor")
        {
            gameObject.tag = "Meteor";
            Debug.Log("Set meteor tag on: " + gameObject.name);
        }
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Cria um alvo que preserva o valor de z do meteoro
            Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Projectile"))
        {
            Debug.Log("Meteor hit by projectile!");
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Meteor took damage! Health: " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}