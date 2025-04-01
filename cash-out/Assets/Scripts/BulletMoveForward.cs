using System;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Pool;

public class BulletMoveForward : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet

    private Rigidbody rb; // Reference to the Rigidbody component

    [SerializeField]
    private ParticleSystem bulletImpactEffect; // Reference to the bullet impact effect (optional)

    [SerializeField]
    public GameObject shooter;

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

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // Destroy the bullet when it collides with any object
        if (collision.gameObject.CompareTag("Enemy")) // Check if the collided object has the tag "Enemy"
        {
            EnemyNPCController enemy = collision.gameObject.GetComponent<EnemyNPCController>(); // Get the EnemyNPCController component from the collided enemy object
            if (enemy != null)
            {
                enemy.GetDamaged(); // Call the GetDamaged method on the enemy to apply damage
            }
            else
            {
                Debug.LogWarning(
                    "EnemyNPCController component not found on the collided enemy object."
                );
            }

            Debug.Log("Hit an enemy!"); // Log that an enemy was hit
        }
        else if (collision.gameObject.CompareTag("Player")) // Check if the collided object has the tag "Player"
        {
            Debug.Log("Hit the player!"); // Log that the player was hit
            // You can add logic here to apply damage to the player or trigger any other effects
        }
        else
        {
            ContactPoint contactPoint = collision.contacts[0]; // Access the first contact point
            Instantiate(
                bulletImpactEffect,
                contactPoint.point,
                Quaternion.LookRotation(shooter.transform.position - contactPoint.point) // Calculate the rotation based on the direction from the impact point to the shooter);
            ); // Instantiate the bullet impact effect at the collision point with the correct rotation
            bulletImpactEffect.Play(); // Play the bullet impact effect if it's assigned
            Debug.Log("Collision at point: " + contactPoint.point); // Log the collision point
        }
    }
}
