using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{    
    public float moveSpeed = 5.0f;
    public float jumpForce = 7.0f;
    public float mouseSensitivity = 2.0f;
    private bool isGrounded = true;

    private float rotationX = 0;

    // Update is called once per frame
    void Update()
    {
        // Rotate the player based on mouse input
        /* float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -90, 90);

        transform.Rotate(Vector3.up * mouseX);
        this.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
 */
        // Calculate the forward and right directions based on the current camera orientation
        Vector3 moveDirection = this.transform.forward;
        Vector3 strafeDirection = this.transform.right;

        // Move the player based on WASD keys
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-strafeDirection * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(strafeDirection * moveSpeed * Time.deltaTime, Space.World);
        }

        // Jump when the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Check if the player is on the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; 
        }
    }
}
