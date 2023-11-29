using UnityEngine;

public class DuoButtonController : MonoBehaviour
{
    public GameObject door;          // door
    public GameObject otherButton;   // other button to be pressed
    private bool isPressed = false;  // check if this button is pressed
    private bool otherButtonPressed = false; // check if the other button is pressed

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = true;
        }

        if (other.CompareTag("Bullet"))
        {
            isPressed = true;
            Destroy(other.gameObject);
        }

        CheckButtons();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = false;
        }

        CheckButtons();
    }

    void CheckButtons()
    {
        if (isPressed && otherButtonPressed)
        {
            OpenDoorAndDestroy();
        }
    }

    void OpenDoorAndDestroy()
    {
        door.SetActive(false); 
        
        Destroy(gameObject);
    }

    // Called by the other button when it is pressed
    public void OtherButtonPressed()
    {
        otherButtonPressed = true;
        CheckButtons();
    }

    // Called by the other button when it is released
    public void OtherButtonReleased()
    {
        otherButtonPressed = false;
    }
}
