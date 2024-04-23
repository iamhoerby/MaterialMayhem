using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs; 

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
    //GameObject ammo;
    customMaterial[] materials = {
    customMaterial.Default, 
    customMaterial.Honey, 
    customMaterial.Rubber, 
    customMaterial.Ice, 
    customMaterial.Metal, 
    customMaterial.Cardbord};

    void Start()
    {
        //ammo = GameObject.Find("materialgunL/Ammo");
        
 
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetComponent<ToolHandler>().currentTool == tool.MaterialGun) {
            var ammo = GameObject.Find("materialgunL/Ammo");
            
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
                // PrintHierarchy();
                if (ammo) {
                    foreach(Transform child in ammo.transform) {
                        child.gameObject.SetActive(true); // or false
                    }
                }
            } else if (ammo) {
                if (ammo.activeSelf) {
                    foreach(Transform child in ammo.transform) {
                        child.gameObject.SetActive(false); // or false
                    }
                }
            } 
            var xrcontroller = GameObject.Find("materialgun/Sphere"); 
            currentMaterial = xrcontroller.transform.GetComponent<BulletHandler>().GetMaterial(); 
        }
    }
    void shoot() {
        var instance = Instantiate(bullet,transform.position + transform.forward,Quaternion.identity);
        instance.GetComponent<BulletHandler>().SetMaterial(currentMaterial); 
        instance.GetComponent<Rigidbody>().AddForce(transform.forward * speed,ForceMode.Force);
    }
}
