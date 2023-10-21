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
    public customMaterial ground; 
    public float speedVariable = 1.0f;
    public float jumpVariable = 1.0f; 

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
            rb.velocity += new Vector3(move.x * moveSpeed * speedVariable, 0, 0);
        }
        if (Mathf.Abs(rb.velocity.z) < maxSpeed)
        {
            rb.velocity += new Vector3(0, 0, move.z * moveSpeed * speedVariable);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce * jumpVariable, ForceMode.Impulse);
        }
        //Update Ground
        ground = GetComponentInChildren<GroundSensorHandler>().GetMaterial(); 
        if (ground != customMaterial.None) {
            isGrounded = true; 
        } else {
            isGrounded = false; 
        }
        GroundEffect(); 
        
    }
    private void GroundEffect() {
         switch (ground)
        {
            case customMaterial.Default: 
                speedVariable = 1.0f; 
                jumpVariable = 1.0f;
                break;
            case customMaterial.Honey: 
                speedVariable = 1.0f;
                jumpVariable = 0.2f; 
                break;
            case customMaterial.Rubber: 
                speedVariable = 1.0f;
                jumpVariable = 1.0f;
                break;
            case customMaterial.Ice: 
                speedVariable = 2.0f;
                jumpVariable = 1.0f; 
                break;
            case customMaterial.Metal: 
                speedVariable = 1.0f;
                jumpVariable = 1.0f; 
                break;
            case customMaterial.Cardbord: 
                speedVariable = 1.0f;
                jumpVariable = 1.0f; 
                break;
            default: 
                speedVariable = 1.0f;
                jumpVariable = 1.0f; 
                break; 
        }
    }
}
