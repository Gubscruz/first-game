using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public AudioClip hitSound;
    
    private Rigidbody2D rb;
    private AudioSource audioSource;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Add AudioSource if needed
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = 0.5f;
        }
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
            // Get meteor component and apply damage
            Meteor meteor = collision.GetComponent<Meteor>();
            if (meteor != null)
            {
                meteor.TakeDamage(damage);
            }
            
            // Play hit sound
            PlayHitSound();
            
            // Destroy the projectile
            Destroy(gameObject);
        }
    }
    
    private void PlayHitSound()
    {
        if (hitSound != null)
        {
            // Create a new GameObject just for playing the sound
            GameObject soundObject = new GameObject("HitSound");
            soundObject.transform.position = transform.position;
            
            // Add an AudioSource to it
            AudioSource soundSource = soundObject.AddComponent<AudioSource>();
            soundSource.clip = hitSound;
            soundSource.volume = 0.5f;
            soundSource.spatialBlend = 0; // 2D sound
            soundSource.Play();
            
            // Destroy the sound object after the clip is done playing
            Debug.Log("Playing hit sound with length: " + hitSound.length);
            Destroy(soundObject, hitSound.length + 0.1f);
        }
        else
        {
            Debug.LogWarning("No hit sound assigned to projectile!");
        }
    }
}