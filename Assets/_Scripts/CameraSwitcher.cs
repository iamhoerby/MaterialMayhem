using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;

    private bool isThirdPerson;

    void Start()
    {
        isThirdPerson = false; // Start in first-person
        firstPersonCamera.enabled = true;
        thirdPersonCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Change this to your preferred key
        {
            isThirdPerson = !isThirdPerson;
            firstPersonCamera.enabled = !isThirdPerson;
            thirdPersonCamera.enabled = isThirdPerson;
            Debug.Log("Camera switched to " + (isThirdPerson ? "Third Person" : "First Person"));
        }
    }
}
