using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    private bool isPressed = false; // To check if the button is pressed
    public string newText;  // Text specific to this button
    public Text textToBeChanged;  // Reference to the UI text object
    public UnityEvent buttonFunction;
    public bool bulletCanHit; 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Morphable"))
        {
            Debug.Log("Trigger" + other.transform.gameObject.name);
            isPressed = true;
            // You can add visual feedback here, e.g., change color or scale of the button
            textToBeChanged.text = newText;
        }

        if (other.CompareTag("Bullet") && bulletCanHit)
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
            buttonFunction.Invoke(); 
            //activatedObject.SetActive(false); // For simplicity, this deactivates the door, you might want to animate it instead
            isPressed = false; 
        }
    }
    public void isClicked() {
        Debug.Log("is pressed");
        isPressed = true; 
    }
    public void openDoor(GameObject Door) {
        Door.SetActive(false);
        //textToBeChanged.text = newText;
    }
    public void restartLevel() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
