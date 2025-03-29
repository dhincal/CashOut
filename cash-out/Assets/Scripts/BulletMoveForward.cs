using System;
using UnityEditor.Callbacks;
using UnityEngine;

public class BulletMoveForward : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet

    private Rigidbody rb; // Reference to the Rigidbody component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the bullet
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.Translate(Vector3.forward * Time.deltaTime * speed); // Move the bullet forward at a speed of 10 units per second

        Vector3 forward = transform.TransformDirection(Vector3.forward); // Get the forward direction of the bullet

        rb.linearVelocity = forward * speed; // Add force to the bullet in the forward direction

        // Destroy the bullet after 2 seconds to prevent it from existing indefinitely
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Check if the collided object has the tag "Enemy"
        {
            Debug.Log("Collision detected with: " + collision.gameObject.name); // Log the name of the object the bullet collides with
            Destroy(gameObject); // Destroy the bullet when it collides with any object

            Debug.Log("Hit an enemy!"); // Log that an enemy was hit
        }
    }
}
