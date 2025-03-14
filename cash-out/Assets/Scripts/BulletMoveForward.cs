using System;
using UnityEngine;

public class BulletMoveForward : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed); // Move the bullet forward at a speed of 10 units per second
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Log the name of the object the bullet collides with
        Destroy(gameObject); // Destroy the bullet when it collides with any object
        Destroy(collision.gameObject); // Destroy the object it collides with
    }
}
