using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviourPunCallbacks
{
    public Camera playerCamera;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpPower = 1f;
    public float gravity = 2f;
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform shootPoint; // Point from which the projectile is spawned
    private bool isShootingUnlocked = false;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float projectileSpeed = 25f;
 
 
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
 
    public bool canMove = true;
 
    
    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
 
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        #region Handles Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        
 
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
 
        #endregion
 
        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
 
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
 
        #endregion
 
        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);
 
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
 
        #endregion

        #region Handles Shooting

    

        if (isShootingUnlocked && Input.GetMouseButtonDown(0))
        {
            ShootProjectile();
        }
        
        void ShootProjectile()
        {
            // Get the rotation of the camera
            Quaternion cameraRotation = Camera.main.transform.rotation;

            // Spawn a new projectile with the same rotation as the camera
            GameObject newProjectile = Instantiate(projectilePrefab, shootPoint.position, cameraRotation);
            newProjectile.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * projectileSpeed,ForceMode.Force);
        }

        #endregion
    }

    
        public void UnlockShooting()
        {
            isShootingUnlocked = true;
        }

    
}
 