using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 1f)
        {
            Destroy(gameObject); // Destroy the ball
        }
    }
}
