using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 


[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public GameObject head;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpPower = 1f;
    public float gravity = 2f;
    public Transform shootPoint; // Point from which the projectile is spawned
    public GameObject bullet;
    public customMaterial currentMaterial;
    public bool isShootingUnlocked = false;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float projectileSpeed = 25f;
    public tool currentTool = tool.MaterialGun; 
    public GameObject toolUiMaterialGun; 
    public GameObject toolUiGrablingGlove; 
    public GameObject materialUI; 


    public GameObject[] uiElements; // Array to store UI elements (max 6)
    public Text currentBullet; // Text UI component 
    public string[] bulletNames; // Array of text options 
    public customMaterial[] bulletMaterials;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
 
    public bool canMove = true;
 
    
    CharacterController characterController;
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UpdateToolUI(); 
    }

    
 
    void Update()
    {
        #region Handles Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        
 
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.O);
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
        if (Input.GetButton("Restart")) {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
 
        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);
 
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            head.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
 
        #endregion

        #region Handles Shooting

        if (isShootingUnlocked && Input.GetMouseButtonDown(0) && currentTool == tool.MaterialGun)
        {
            ShootProjectile();
        }
        
        void ShootProjectile()
        {
            // Get the rotation of the camera
            Quaternion headRotation = head.transform.rotation;

            // Spawn a new projectile with the same rotation as the head
            GameObject newProjectile = Instantiate(bullet, shootPoint.position, headRotation);
            newProjectile.GetComponent<BulletHandler>().SetMaterial(currentMaterial); 
            newProjectile.GetComponent<Rigidbody>().AddForce(head.transform.forward * projectileSpeed,ForceMode.Force);
            
            
        }

        #endregion
        #region Handles changing tool

        if (Input.GetMouseButtonDown(1)) {
            if (currentTool == tool.MaterialGun) {
                currentTool = tool.GrablingGloves; 
            } else {
                currentTool = tool.MaterialGun; 
            }
        }
        UpdateToolUI(); 

        #endregion
        #region Handles toolbar and changing tool

        for (int i = 0; i < uiElements.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Check keys 1 to 6
                {
                    for (int j = 0; j < uiElements.Length; j++)
                    {
                        uiElements[j].SetActive(j == i); // Set active only for the pressed key
                    }
                    currentBullet.text = bulletNames[i]; 
                    currentMaterial = bulletMaterials[i];
                }
        }
        
        #endregion

    }

    public void UnlockShooting()
    {
        isShootingUnlocked = true;
    }
    void UpdateToolUI() {
        if (!isShootingUnlocked) {
            materialUI.SetActive(false);
            toolUiMaterialGun.SetActive(false);
            toolUiGrablingGlove.SetActive(false);
        } else if (currentTool == tool.MaterialGun) {
            toolUiMaterialGun.SetActive(true);
            toolUiGrablingGlove.SetActive(false);
            materialUI.SetActive(true);
        } else {
            toolUiMaterialGun.SetActive(false);
            toolUiGrablingGlove.SetActive(true);
            materialUI.SetActive(false);
        } 
    }
}

 