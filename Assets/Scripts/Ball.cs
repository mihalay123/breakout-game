using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialSpeed = 300f;
    public float constantSpeed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized * constantSpeed;

    }

    void Update()
    {
        // Check if the ball's position is below the bottom of the screen
        if (transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 1f)
        {
            Destroy(gameObject); // Destroy the ball
        }
    }

    void FixedUpdate()
    {
        MaintainConstantSpeed();
    }

    private void MaintainConstantSpeed()
    {
        Debug.Log($"Maintaining constant speed {rb.linearVelocity.magnitude}");
        if (rb.linearVelocity.magnitude != 0)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * constantSpeed;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Ignore collision logic here
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
