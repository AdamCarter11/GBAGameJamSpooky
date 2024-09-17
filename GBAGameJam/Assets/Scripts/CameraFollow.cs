using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector2 offset = new Vector2(0f, 2f); // Offset from the player's position
    public float smoothSpeed = 0.125f; // Speed of the camera's smooth movement

    private Vector3 targetPosition;

    void FixedUpdate()
    {
        // Set the target position based on player's position with the offset
        targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -10f); // Keep z constant for 2D

        // Smoothly move the camera to the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;
    }
}
