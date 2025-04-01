using UnityEngine;

public class EnemyNPCController : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100; // Health of the enemy NPC

    [SerializeField]
    private GameObject player; // Reference to the player object, if needed for aiming or other logic

    [SerializeField]
    private GameObject gun;

    private GunController gunController; // Reference to the GunController component

    private int currentHealth; // Current health of the enemy NPC

    private FieldOfView fovController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
        gunController = gun.GetComponent<GunController>();
        fovController = gameObject.GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update() { }

    public void CallAuthorities()
    {
        // Handle the logic for calling authorities here, such as alerting other NPCs or triggering an event
        Debug.Log("Authorities have been called!");
        // You can add more logic here, like changing the state of the game or notifying other components
    }

    private void Die()
    {
        // Handle the death of the enemy NPC here, such as playing an animation, dropping loot, etc.
        Debug.Log("Enemy has died!");
        Destroy(gameObject); // Destroy the enemy NPC game object
    }

    public void GetDamaged()
    {
        int headShotChance = Random.Range(0, 4); // Random chance to get headshot

        if (headShotChance == 3)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= 33; // Take damage, for example, 33 damage per hit
        }

        if (currentHealth <= 0)
        {
            Die(); // Call the die function if health is 0 or less
        }
    }

    public void AimToShoot()
    {
        Debug.Log("Enemy is shooting!");

        // Aim to Player

        if (player != null)
        {
            float aimMistakeOffset = Random.Range(0, 0); // Randomize aim mistake offset

            // Optionally, you can add a slight offset to the aim if needed
            Vector3 aimOffset = new Vector3(0, aimMistakeOffset, 0); // Adjust the Y offset if needed

            // For example, if you have a GunController script attached to the enemy:
            if (gunController != null)
            {
                if (fovController.seeSuspicous)
                {
                    gunController.Shoot(); // Call the shoot method from the GunController
                }
                else
                {
                    Debug.Log("Enemy NPC is not in a position to shoot (not seeing the player).");
                }
            }
            else
            {
                Debug.LogWarning("GunController not found on the enemy NPC!");
            }
        }
        else
        {
            Debug.LogWarning("Player reference is not set!");
        }
    }
}
