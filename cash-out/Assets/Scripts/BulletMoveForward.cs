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

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // Destroy the bullet when it collides with any object
    }
}
