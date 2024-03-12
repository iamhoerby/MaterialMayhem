using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject door; // Reference to the door object
    private bool isPressed = false; // To check if the button is pressed
    public string newText;  // Text specific to this button
    public Text textToBeChanged;  // Reference to the UI text object
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger" + other.transform.gameObject.name);
            isPressed = true;
            // You can add visual feedback here, e.g., change color or scale of the button
            textToBeChanged.text = newText;
        }

        if (other.CompareTag("Bullet"))
        {
            isPressed = true;
            // You can add visual feedback here, e.g., change color or scale of the button
            // Optionally, you might want to destroy the projectile when it hits the button
            Destroy(other.gameObject);
            textToBeChanged.text = newText;
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
