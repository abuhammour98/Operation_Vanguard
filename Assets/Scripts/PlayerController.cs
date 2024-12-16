using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float turnSpeed = 100f; // Turn speed for E and Q keys

    void Update()
    {
        // Handle rotation
        float turnInput = 0f;
        if (Input.GetKey(KeyCode.E)) turnInput = 1f; // Turn right when E is held
        if (Input.GetKey(KeyCode.Q)) turnInput = -1f; // Turn left when Q is held
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);

        // Handle forward/backward movement using player's forward direction
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.W)) moveInput = 1f; // Move forward
        if (Input.GetKey(KeyCode.S)) moveInput = -1f; // Move backward

        // Move the player in their current forward direction
        Vector3 moveDirection = transform.forward * moveInput;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
