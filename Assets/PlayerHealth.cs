using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    [HideInInspector]
    public int currentHealth;
    
    [Header("Visual Effects")]
    public float invincibilityDuration = 1.0f;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        // Don't take damage if invincible
        if (isInvincible)
            return;
            
        currentHealth -= damage;
        
        // Start visual feedback
        StartCoroutine(FlashEffect());
        StartCoroutine(InvincibilityFrames());
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private IEnumerator FlashEffect()
    {
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
        }
        else
        {
            yield return null;
        }
    }
    
    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        
        // Flash effect to indicate invincibility
        if (spriteRenderer != null)
        {
            float endTime = Time.time + invincibilityDuration;
            while (Time.time < endTime)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSeconds(0.1f);
            }
            spriteRenderer.enabled = true;
        }
        else
        {
            yield return new WaitForSeconds(invincibilityDuration);
        }
        
        isInvincible = false;
    }
    
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    void Die()
    {
        Debug.Log("Player morreu.");
        Destroy(gameObject);
    }
}