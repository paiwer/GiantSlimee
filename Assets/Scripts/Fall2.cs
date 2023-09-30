using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall2 : MonoBehaviour
{
    public float damageThreshold = 10f; // Velocity threshold for taking damage
    public float damageMultiplier = 1f; // Multiplier for damage taken

    private Rigidbody playerRigidbody;
    private bool isFalling = false;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the player is falling (negative vertical velocity)
        if (playerRigidbody.velocity.y < 0)
        {
            // Calculate the falling velocity (positive value)
            float fallingVelocity = -playerRigidbody.velocity.y;

            // Check if the falling velocity exceeds the threshold
            if (fallingVelocity > damageThreshold)
            {
                // Player is falling
                isFalling = true;
            }
        }
        else
        {
            // Player is not falling
            if (isFalling)
            {
                // Calculate the damage based on falling velocity
                float damage = (playerRigidbody.velocity.y - damageThreshold) * damageMultiplier;

                // Apply damage to the player (you can replace this with your own damage system)
                ApplyDamage(damage);

                isFalling = false; // Reset the falling flag
            }
        }
    }

    private void ApplyDamage(float damage)
    {
        // Replace this with your damage logic (e.g., reducing player health)
        Debug.Log("Player took " + damage + " damage!");
    }
}
