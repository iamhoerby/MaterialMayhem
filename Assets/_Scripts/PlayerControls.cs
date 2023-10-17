using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{    
    public float moveSpeed = 5.0f;
    public float maxSpeed;
    public float jumpForce = 7.0f;
    private bool isGrounded = true;
    private Rigidbody rb;
    private Vector3 move;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
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
        move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.velocity += new Vector3(move.x * moveSpeed, 0, 0);
        }
        if (Mathf.Abs(rb.velocity.z) < maxSpeed)
        {
            rb.velocity += new Vector3(0, 0, move.z * moveSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }

    // Check if the player is on the ground
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
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
