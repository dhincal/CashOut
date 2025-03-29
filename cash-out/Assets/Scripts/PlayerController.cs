using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [SerializeField]
    private GameObject gun;

    private GameObject flashlight; // Variable to store the flashlight object
    private float horizontalInput;
    private float verticalInput;
    private float speed = 10.0f;

    private Vector2 mousePosition; // Variable to store mouse position
    public float mouseX; // Variable to store mouse X movement

    void Start()
    {
        // Find the flashlight object in the scene by its name
        flashlight = GameObject.Find("Flashlight");

        if (flashlight != null)
        {
            // If the flashlight is found, enable it
            flashlight.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Flashlight not found in the scene!");
        }

        // Ensure the gun is assigned and has a GunController component
        if (gun == null || gun.GetComponent<GunController>() == null)
        {
            Debug.LogError(
                "Gun object is not assigned or does not have a GunController component!"
            );
        }
    }

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

        if (Input.GetKeyDown(KeyCode.R)) // Check if the R key is pressed
        {
            gun.GetComponent<GunController>().Reload(); // Call the Reload method from the GunController script
        }

        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button is pressed
        {
            gun.GetComponent<GunController>().Shoot(); // Call the Shoot method from the GunController script
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            // Toggle the flashlight on and off when the F key is pressed
            if (flashlight != null)
            {
                flashlight.SetActive(!flashlight.activeSelf); // Toggle the active state of the flashlight
            }
            else
            {
                Debug.LogWarning("Flashlight not found in the scene!");
            }
        }
    }
}
