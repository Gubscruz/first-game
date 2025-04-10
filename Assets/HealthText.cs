using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TMP_Text healthText;

    void Start()
    {
        // If playerHealth is not assigned, try to find it in the scene
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerHealth = player.GetComponent<PlayerHealth>();
            }
        }

        // If healthText is not assigned, try to get it from this GameObject
        if (healthText == null)
        {
            healthText = GetComponent<TMP_Text>();
        }
    }

    void Update()
    {
        if (playerHealth != null && healthText != null)
        {
            healthText.text = "Vidas restantes: " + playerHealth.currentHealth;
        }
    }
}