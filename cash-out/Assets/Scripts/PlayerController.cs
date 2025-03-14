using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public GameObject bullet; // Prefab of the bullet to be instantiated

    private float horizontalInput;
    private float verticalInput;
    private float speed = 10.0f;

    private Vector2 mousePosition; // Variable to store mouse position
    public float mouseX; // Variable to store mouse X movement

    void Start() { }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // Move the player based on input disregarding players rotation
        transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput, Space.World);
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput, Space.World);

        var mousePosition = Input.mousePosition; // Get the current mouse position
        var wantedPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y)
        ); // Convert screen position to world position
        transform.LookAt(wantedPosition); // Make the player look at the mouse position
        var rotation = transform.rotation;
        rotation.x = 0; // Reset the x rotation to 0
        rotation.z = 0; // Reset the z rotation to 0
        transform.rotation = rotation;

        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button is pressed
        {
            Shoot(); // Call the Shoot method to shoot a bullet
        }
    }

    void Shoot()
    {
        // Create a bullet and set its position and rotation
        Instantiate(bullet, transform.position + transform.forward * 2, transform.rotation);
        bullet.transform.position = transform.position + transform.forward * 2; // Set the bullet's position to be in front of the player
        bullet.transform.rotation = transform.rotation; // Set the bullet's rotation to match the player's rotation
    }
}
