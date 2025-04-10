using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    [HideInInspector]
    public int currentHealth;
    
    [Header("Visual Effects")]
    public float invincibilityDuration = 1.0f;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    
    [Header("Game Over")]
    public string gameOverSceneName = "GameOver"; // Name of your GameOver scene

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
        
        // Store score data for the GameOver scene
        if (ScoreManager.instance != null)
        {
            // Get the private score field using reflection
            var scoreField = typeof(ScoreManager).GetField("score", 
                System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Instance);
                
            if (scoreField != null)
            {
                GameOverData.score = (int)scoreField.GetValue(ScoreManager.instance);
            }
        }
        
        // Store time data
        Chronometer chronometer = FindObjectOfType<Chronometer>();
        if (chronometer != null)
        {
            // Get the private elapsedTime field using reflection
            var timeField = typeof(Chronometer).GetField("elapsedTime", 
                System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Instance);
                
            if (timeField != null)
            {
                GameOverData.timeAtDeath = (float)timeField.GetValue(chronometer);
            }
        }
        else
        {
            // Fallback if chronometer not found
            GameOverData.timeAtDeath = Time.time;
        }
        
        // Load the GameOver scene
        SceneManager.LoadScene(gameOverSceneName);
        
        // Destroy the player object
        Destroy(gameObject);
    }
}