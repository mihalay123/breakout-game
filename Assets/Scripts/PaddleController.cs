using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float speedup = 10f;
    [SerializeField] private bool withInertia = false; // Half of the paddle's width
    private float paddleWidth; // Half of the paddle's width
    private Vector2 screenBounds;


    // Start is called before the first frame update
    void Start()
    {
        paddleWidth = GetComponent<SpriteRenderer>().bounds.extents.x;

        // Calculate the screen bounds in world coordinates
        screenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    // Update is called once per frame
    void Update()
    {
        float input = withInertia ? Input.GetAxis("Horizontal") : Input.GetAxisRaw("Horizontal");
        Vector3 newPosition = transform.position;
        newPosition.x += input * speedup * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -screenBounds.x + paddleWidth, screenBounds.x - paddleWidth);
        transform.position = newPosition;
    }

}
