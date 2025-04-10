using UnityEngine;

public class PendulumBaseControll : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 pos = rb.position;
        pos.x += moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(pos);
    }
}