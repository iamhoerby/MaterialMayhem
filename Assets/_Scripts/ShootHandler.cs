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

    void switchMaterial() {
        



       /*  int currentIndex = 0; 
        foreach (var material in materials)
        {
            if (currentMaterial == material) { 
                currentIndex++; 
                if (currentIndex >= materials.Length) {
                    currentIndex = 0; 
                } 
                currentMaterial = materials[currentIndex];
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
        } */
    }

static void PrintHierarchy()
{
    Debug.Log("Print object hierarchy");
    foreach (var obj in SceneManager.GetActiveScene().GetRootGameObjects())
    {
        Debug.Log("Root object");
        PrintChildren(obj.transform, "");
    }
}

static void PrintChildren(Transform t, string indent)
{
    int child_count = t.childCount;
    Debug.Log($"{indent}'{t.name}' has {child_count} children");

    var more_indent = indent + "	";
    for (int i = 0; i < child_count; ++i)
    {
        var child = t.GetChild(i);
        PrintChildren(child, more_indent);
    }
}
}
