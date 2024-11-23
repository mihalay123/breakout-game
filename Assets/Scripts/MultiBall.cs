using UnityEditor.UIElements;
using UnityEngine;

public class MultiBall : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] int multiplier = 3;
    [SerializeField] int maxBalls = 100;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paddle"))
        {
            MultiplyBalls();
            Destroy(gameObject);
        }
    }

    void MultiplyBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            for (int i = 0; i < multiplier; i++)
            {
                int curentBallCount = GameObject.FindGameObjectsWithTag("Ball").Length;
                if (curentBallCount >= maxBalls)
                {
                    return;
                }
                GameObject newBall = Instantiate(ballPrefab, ball.transform.position, Quaternion.identity);

                newBall.transform.parent = ball.transform.parent;

                // Assign a random velocity to the new ball
                Rigidbody2D newBallRb = newBall.GetComponent<Rigidbody2D>();
                Rigidbody2D originalBallRb = ball.GetComponent<Rigidbody2D>();

                if (newBallRb != null && originalBallRb != null)
                {
                    // Add some randomness to differentiate new balls
                    newBallRb.linearVelocity = originalBallRb.linearVelocity + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    // newBallRb.linearVelocity = originalBallRb.linearVelocity;
                }
            }

        }
    }
}
