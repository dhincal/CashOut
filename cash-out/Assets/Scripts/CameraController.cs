using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    private Vector3 offset; // Offset from the player position

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = new Vector3(0, 8.0f, 0);
        transform.position = player.transform.position + offset; // Set initial camera position based on player position and offset
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset; // Update camera position based on player position and offset
        transform.LookAt(player.transform); // Make the camera look at the player
    }
}
