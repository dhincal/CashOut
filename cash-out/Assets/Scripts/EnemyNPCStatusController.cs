using UnityEngine;

public class EnemyNPCStatusController : MonoBehaviour
{
    public int health = 100; // Health of the enemy NPC

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Bullet"))
    //     {
    //         Destroy(other.gameObject); // Destroy the bullet on collision
    //         // Logic for when the enemy collides with the player
    //         health -= 33; // Decrease health by 33 on collision with a bullet
    //         if (health <= 0)
    //         {
    //             Destroy(gameObject); // Destroy the enemy NPC if health is 0 or less
    //         }
    //     }
    // }
}
