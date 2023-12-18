using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PullHandler : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float speed; 
    public InputActionProperty pullAction;
    Transform controller; 
    bool pullActive = false;
    customMaterial honey = customMaterial.Honey; 
    public GameObject pullObject; 

    void Start()
    {
        controller = transform; 
    }

    // Update is called once per frame
    void Update()
    {
        selectObject(); 
        if (pullAction.action.IsPressed()) {
            if (!pullActive) {
                pullActive = true; 
                pull();
            }           
        } else {
            if (pullActive) {
                pullActive = false; 
            }
        }
        
    }
    void selectObject() {
        RaycastHit[] hits; 
        hits = Physics.RaycastAll(controller.position, controller.forward , 100.0f);
        
        if (hits[0].collider.tag == "Morphable") {
            if (hits[0].collider.gameObject.GetComponent<MaterialImpactHandler>().GetMaterial() != honey) {
                pullObject = hits[0].collider.gameObject; 
                //Highlight Object here 
            } else {
                pullObject = null; 
            }   
        } else {
            pullObject = null; 
        }
    }
    void pull() {
        Vector3 pullDirection = controller.transform.position - pullObject.transform.position; 
        pullObject.GetComponent<Rigidbody>().AddForce(pullDirection * speed, ForceMode.Force); 
    }
}
