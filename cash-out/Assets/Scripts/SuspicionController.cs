using UnityEngine;

public class SuspicionController : MonoBehaviour
{
    public float suspicionLevel = 0f; // Current suspicion level of the NPC
    public float maxSuspicionLevel = 100f; // Maximum suspicion level before the NPC becomes suspicious or alerts the player
    public float suspicionIncreaseRate = 100f; // Rate at which suspicion increases per second when the player is seen or heard
    public float suspicionDecreaseRate = 5f; // Rate at which suspicion decreases per second when the player is not seen or heard

    public MonoBehaviour fieldOfView; // Reference to the FieldOfView script to check if the player is seen or heard

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is seen or heard using the FieldOfView script
        if (fieldOfView.GetType().GetField("seeSuspicous").GetValue(fieldOfView).Equals(true))
        {
            float distToTarget = (float)
                fieldOfView.GetType().GetField("distToTarget").GetValue(fieldOfView); // Get the distance to the target from the FieldOfView script
            // Increase suspicion level based on the suspicion increase rate and time.deltaTime
            suspicionLevel += suspicionIncreaseRate * Time.deltaTime * (5 / distToTarget); // Increase suspicion faster if the player is closer
            if (suspicionLevel > maxSuspicionLevel) // Ensure suspicion level does not exceed the maximum level
            {
                suspicionLevel = maxSuspicionLevel;
            }
        }
        else
        {
            // Decrease suspicion level based on the suspicion decrease rate and time.deltaTime
            suspicionLevel -= suspicionDecreaseRate * Time.deltaTime; // Decrease suspicion faster if the player is not seen or heard recently
            if (suspicionLevel < 0f) // Ensure suspicion level does not go below 0
            {
                suspicionLevel = 0f;
            }
        }
    }
}
