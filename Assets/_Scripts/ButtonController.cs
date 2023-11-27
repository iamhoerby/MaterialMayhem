using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject door; // Reference to the door object
    private bool isPressed = false; // To check if the button is pressed

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = true;
            // You can add visual feedback here, e.g., change color or scale of the button
        }

        if (other.CompareTag("Bullet"))
        {
            isPressed = true;
            // You can add visual feedback here, e.g., change color or scale of the button
            // Optionally, you might want to destroy the projectile when it hits the button
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = false;
            // Reset any visual feedback
        }
    }

    void Update()
    {
        if (isPressed)
        {
            // Open the door (you can animate or move it)
            door.SetActive(false); // For simplicity, this deactivates the door, you might want to animate it instead
        }
    }
}
