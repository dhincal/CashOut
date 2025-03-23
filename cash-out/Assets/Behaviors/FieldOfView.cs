using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    public float viewDistance; // Distance at which the enemy can see the player

    [Range(0, 360)] // Angle of the field of view
    public float viewAngle;

    public LayerMask targetMask; // Layer mask to identify the target (player)
    public LayerMask obstacleMask; // Layer mask to identify obstacles
    public bool seeSuspicous = false; // Flag to indicate if the enemy can see suspicious objects

    public Vector3 dirToTarget;
    public float distToTarget; // Distance to the target (player)

    void Start()
    {
        StartCoroutine(FOVRoutine()); // Start the field of view check routine
    }

    // Update is called once per frame
    void Update() { }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.2f); // Wait time between checks

        while (true)
        {
            yield return waitTime; // Wait for the specified time before checking again
            FieldOfViewCheck(); // Check if the player is within the field of view
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(
            transform.position,
            viewDistance,
            targetMask
        ); // Check for colliders within the view distance
        if (rangeChecks.Length > 0) // If there are colliders within the view distance
        {
            Transform target = rangeChecks[0].transform; // Get the first collider's transform
            dirToTarget = (target.position - transform.position).normalized; // Calculate direction to the target
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) // Check if the target is within the field of view angle
            {
                distToTarget = Vector3.Distance(transform.position, target.position); // Calculate distance to the target
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask)) // Check for obstacles between the enemy and the target
                {
                    seeSuspicous = true; // Set the flag to true if the target is visible
                    Debug.Log("Player detected!"); // Log that the player is detected
                }
                else
                {
                    seeSuspicous = false; // Set the flag to false if there are obstacles
                    Debug.Log("Obstacle detected!"); // Log that an obstacle is detected
                }
            }
            else
            {
                seeSuspicous = false; // Set the flag to false if the target is outside the field of view angle
            }
        }
        else
        {
            seeSuspicous = false;
            Debug.Log("No player detected!"); // Log that no player is detected
        }
    }
}
