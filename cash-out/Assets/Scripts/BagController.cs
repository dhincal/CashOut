using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BagController : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private float holdTime = 2f; // Time in seconds the player must hold the 'E' key to steal the item
    private bool isInRadius = false;

    [SerializeField]
    private GameObject player; // Reference to the player object (optional, can be used for more complex logic)

    [SerializeField]
    private Enum weightOfBag; // Enum to define the weight of the bag, can be used for inventory management (optional)

    public enum WeightOfBag
    {
        Light,
        Medium,
        Heavy,
    }

    private PlayerController playerController; // Reference to the PlayerController script (if needed)
    private bool isCarrying = false; // Track if the player is currently carrying the bag

    private Rigidbody rb; // Reference to the Rigidbody component for applying physics (if needed)
    private Collider itemCollider; // Reference to the Collider component for enabling/disabling it during carry

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = player.GetComponent<PlayerController>(); // Get the PlayerController component from the player object
        rb = gameObject.GetComponent<Rigidbody>();
        itemCollider = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRadius)
        {
            // Check if the player is holding the 'E' key to attempt stealing the item
            if (Input.GetKeyDown(KeyCode.E) && !isCarrying)
            {
                StartCoroutine(TakeBag()); // Start the coroutine to handle stealing the item
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && isCarrying) // Allow player to drop the bag if they are carrying it
        {
            // Allow player to drop the bag by pressing 'E' again
            Debug.Log($"Player has dropped {itemName}.");
            StopCoroutine(CarryBag()); // Stop the carry coroutine if it's running
            gameObject.transform.SetParent(null); // Remove the parent to drop the bag
            gameObject.transform.position = player.transform.position + new Vector3(0, 0, 2); // Drop the bag in front of the player (optional)
            // Apply force for throwing the bag


            if (rb != null)
            {
                rb.isKinematic = false; // Make sure the Rigidbody is not kinematic to apply physics
                rb.AddForce(player.transform.forward * 10f, ForceMode.Impulse); // Apply a forward force to simulate throwing
            }
            itemCollider.enabled = true; // Re-enable the collider for future interactions
            isCarrying = false; // Mark the player as not carrying the bag anymore
        }
    }

    IEnumerator TakeBag()
    {
        float timer = 0f;

        while (timer < holdTime && isInRadius) // Ensure the player is still in the radius while holding 'E'
        {
            timer += Time.deltaTime;
            Debug.Log($"Player is attempting to take {itemName}. Time held: {timer:F2}s");
            yield return null; // Wait for the next frame
        }

        if (timer >= holdTime && isInRadius)
        {
            // Logic to add the item to the player's bag

            Debug.Log($"Player has successfully taken {itemName}!");
            isCarrying = true; // Mark the player as carrying the bag
            StartCoroutine(CarryBag()); // Start the coroutine to handle carrying the bag
            yield break;
        }
    }

    IEnumerator CarryBag()
    {
        // Logic for carrying the bag can be implemented here
        Debug.Log($"Player is now carrying {itemName}.");

        gameObject.transform.SetParent(player.transform); // Make the bag a child of the player to follow them around

        gameObject.transform.localPosition = new Vector3(0, 1, -1); // Adjust the position relative to the player (optional)

        itemCollider.enabled = false; // Optionally disable the collider to prevent further interactions
        rb.isKinematic = true; // Make the Rigidbody kinematic to prevent physics from affecting it while being carried

        if (Input.GetKeyDown(KeyCode.G))
        {
            // Allow player to drop the bag by pressing 'E' again
            Debug.Log($"Player has dropped {itemName}.");
            gameObject.transform.SetParent(null); // Remove the parent to drop the bag
            gameObject.transform.position = player.transform.position + new Vector3(0, 0, 2); // Drop the bag in front of the player (optional)
            gameObject.GetComponent<Collider>().enabled = true; // Re-enable the collider for future interactions
            isCarrying = false; // Mark the player as not carrying the bag anymore
            yield break; // Exit the coroutine
        }
    }

    void OnTriggerEnter(Collider other)
    {
        isInRadius = true; // Player has entered the trigger zone for stealing the item
    }

    void OnTriggerExit(Collider other)
    {
        isInRadius = false; // Player has exited the trigger zone, stop the stealing process
    }
}
