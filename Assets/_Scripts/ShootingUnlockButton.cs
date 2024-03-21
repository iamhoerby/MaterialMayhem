using UnityEngine;
using UnityEngine.UI;

public class ShootingUnlockButton : MonoBehaviour

{
    public string newText;  // Text specific to this button
    public GameObject targetObject; // Assign the GameObject you want to toggle
    public Text textToBeChanged;  // Reference to the UI text object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Unlock the shooting ability
            other.GetComponent<FPSController>().UnlockShooting();
            
            // Optionally, you can add visual feedback or deactivate the button
            gameObject.SetActive(false);

            // targetObject.SetActive(!targetObject.activeSelf); // Toggle visibility

            textToBeChanged.text = newText;
        }
    }
}
