using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.SceneManagement; 

public class ShootHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet; 
    public float speed; 
    public InputActionProperty shootAction;
    public InputActionProperty switchMaterialAction; 
    bool switchActive = false; 
    public customMaterial currentMaterial; 
    bool shotActive = false;

    customMaterial[] materials = {
    customMaterial.Default, 
    customMaterial.Honey, 
    customMaterial.Rubber, 
    customMaterial.Ice, 
    customMaterial.Metal, 
    customMaterial.Cardbord};

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shootAction.action.IsPressed()) {
            if (!shotActive) {
                shotActive = true; 
                shoot();
            }           
        } else {
            if (shotActive) {
                shotActive = false; 
            }
        }
        if (switchMaterialAction.action.IsPressed()) {
            if (!switchActive) {
                switchActive = true; 
                switchMaterial();
            }           
        } else {
            if (switchActive) {
                switchActive = false; 
            }
        }
        
    }
    void shoot() {
        var instance = Instantiate(bullet,transform.position + transform.forward,Quaternion.identity);
        instance.GetComponent<BulletHandler>().SetMaterial(currentMaterial); 
        instance.GetComponent<Rigidbody>().AddForce(transform.forward * speed,ForceMode.Force);
    }
    
    void switchMaterial() {
        Debug.Log("SwitchMaterial Start with " + currentMaterial);
        int currentIndex = 1; 
        foreach (var material in materials)
        {
            if (currentMaterial == material) { 
                currentIndex++; 
                if (currentIndex >= materials.Length) {
                    currentIndex = 0; 
                } 
                currentMaterial = materials[currentIndex];
                Debug.Log("Switch to " + currentMaterial);
                var xrcontroller = GameObject.Find("materialgun/Sphere"); 
                xrcontroller.transform.GetComponent<BulletHandler>().SetMaterial(currentMaterial); 
                return; 
            } else {
                currentIndex++; 
                if (currentIndex >= materials.Length - 1) {
                    currentIndex = 0; 
                } 
            }
            if (currentIndex >= materials.Length) {
                currentIndex = 0; 
            }
        }
    }
}
