using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealableItemController : MonoBehaviour
{
    [SerializeField]
    private float itemValue;

    [SerializeField]
    private string itemName;

    [SerializeField]
    private float holdTime = 2f; // Time in seconds the player must hold the 'E' key to steal the item
    private bool isInRadius = false;

    private enum ItemRairty
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
    }

    [SerializeField]
    private ItemRairty itemRarity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (isInRadius)
        {
            // Check if the player is holding the 'E' key to attempt stealing the item
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(StealItem()); // Start the coroutine to handle stealing the item
            }
        }
    }

    IEnumerator StealItem()
    {
        float timer = 0f;

        while (timer < holdTime && isInRadius) // Ensure the player is still in the radius while holding 'E'
        {
            timer += Time.deltaTime;
            Debug.Log($"Player is attempting to steal {itemName}. Time held: {timer:F2}s");

            if (!Input.GetKey(KeyCode.E) || !isInRadius) // Break the loop if the key is released
            {
                timer = 0f; // Reset timer if the key is released
                yield break; // Exit the coroutine
            }
            if (timer >= holdTime) // If the hold time is reached, proceed to steal the item
            {
                Destroy(gameObject); // Exit the loop to proceed with stealing the item
            }

            yield return null; // Wait for the next frame
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Handle the logic when another object collides with this item, e.g., stealing it
        if (other.CompareTag("Player"))
        {
            isInRadius = true; // Set the flag to true when the player enters the trigger area

            // Make player hold the e key for 2 seconds to "steal" the item

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log($"Player is attempting to steal {itemName}");
                float timer = 0f;
                while (timer < holdTime)
                {
                    timer += Time.deltaTime;
                    if (!Input.GetKeyDown(KeyCode.E)) // Break the loop if the key is released
                    {
                        timer = 0f; // Reset timer if the key is released
                        break;
                    }
                }

                Destroy(gameObject); // Destroy the item after it has been "stolen"
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        isInRadius = false; // Reset the flag when the player exits the trigger area
    }
}
