using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarToggle: MonoBehaviour
{
  public GameObject targetObject; // Assign the GameObject you want to toggle

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Alpha1)) // Check if key "1" is pressed down
    {
      targetObject.SetActive(!targetObject.activeSelf); // Toggle visibility
    }
  }
}
