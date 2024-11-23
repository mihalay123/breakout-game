using System;
using System.Security.Cryptography;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int strength = 1; // Number of hits required to destroy the brick
    public bool unbreakable = false; // Flag for unbreakable bricks
    public
    Color[] strengthColors = Colors.strengthColors; // Colors based on the brick's strength
    private SpriteRenderer spriteRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the brick color based on its strength
        UpdateColor();
    }

    void UpdateColor()
    {
        if (unbreakable)
        {
            spriteRenderer.color = Color.gray; // Set unbreakable bricks to gray
        }
        else if (strength > 0 && strength - 1 < strengthColors.Length)
        {
            spriteRenderer.color = strengthColors[strength - 1];
        }
        else
        {
            spriteRenderer.color = Color.white; // Default color
        }
    }

    public void Hit()
    {
        if (unbreakable)
            return;

        strength--;

        if (strength <= 0)
        {
            BrickEventManager.BrickCrashed(transform.position); // Notify the event manager
            Destroy(gameObject); // Destroy the brick
        }
        else
        {
            UpdateColor(); // Update color based on the new strength
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Hit();
            // Destroy(gameObject);
        }
    }
}
