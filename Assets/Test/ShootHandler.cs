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
    bool shotActive = false;

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
        
    }
    void shoot() {
        var instance = Instantiate(bullet,transform.position + transform.forward,Quaternion.identity);
        instance.GetComponent<Rigidbody>().AddForce(transform.forward * speed,ForceMode.Force);
    }
}
