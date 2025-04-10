using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    AudioSource audioSource;
    public float speed = 5.0f;
    private Camera mainCamera;
    
    [Header("Shooting")]
    public GameObject projectilePrefab;
    public float fireRate = 0.2f;
    public Transform firePoint;
    private float nextFireTime = 0f;
    private bool canShoot = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
        
        // If no fire point is assigned, create one at the player's position
        if (firePoint == null)
        {
            firePoint = transform;
        }
        
        // Make sure we have a reference to the main camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("No main camera found! Please tag a camera as MainCamera.");
                canShoot = false;
            }
        }
    }

    void Update()
    {
        // Handle shooting input - only shoot on button down, not continuous hold
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1 button pressed at time: " + Time.time + ", nextFireTime: " + nextFireTime + ", canShoot: " + canShoot);
            
            if (Time.time >= nextFireTime && canShoot)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                Debug.Log("Cannot shoot. Time check: " + (Time.time >= nextFireTime) + ", canShoot: " + canShoot);
            }
        }
    }

    void FixedUpdate()
    {
        // Make sure we still have a reference to the main camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Lost reference to main camera!");
                return;
            }
        }
        
        // Movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
        
        // Orientation - point toward mouse
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)mousePosition - rb.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Subtract 90 degrees to make the top point toward the mouse
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        // Get direction we're facing
        Vector2 shootDirection = transform.up; // Since we rotated to make the top point toward the mouse
        
        // Create the projectile at the fire point
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            
            // Launch the projectile
            Projectile projectileComponent = projectile.GetComponent<Projectile>();
            if (projectileComponent != null)
            {
                projectileComponent.Launch(shootDirection);
                Debug.Log("Shot fired at " + Time.time);
            }
            else
            {
                Debug.LogError("Projectile prefab does not have a Projectile component!");
            }
        }
        else
        {
            Debug.LogError("Projectile prefab is not assigned!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coletavel"))
        {
            audioSource.Play();
            Destroy(collision.gameObject);
        }
    }
}