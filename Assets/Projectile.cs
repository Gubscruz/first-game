using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    
    private Rigidbody2D rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.Log("Projectile " + GetInstanceID() + " launched with velocity " + rb.linearVelocity + " at time " + Time.time);
    }
    
    void Update()
    {
        if (rb.linearVelocity.magnitude < 0.1f)
        {
            Debug.LogWarning("Projectile " + GetInstanceID() + " has stopped moving. Reapplying velocity.");
            rb.linearVelocity = transform.up * speed;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meteor"))
        {
            Meteor meteor = collision.GetComponent<Meteor>();
            if (meteor != null)
            {
                meteor.TakeDamage(damage);
            }
        }
        
        // Agendamento para destruir o projétil 1 segundo após a colisão
        Destroy(gameObject, 1f);
    }
}