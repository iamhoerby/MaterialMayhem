using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class LoadAmmoHandler : MonoBehaviour
{
    public customMaterial currentMaterial; 
    public InputActionProperty switchMaterialAction; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (switchMaterialAction.action.IsPressed()) {
        //if (Input.GetButton("Jump")) {
            //RaycastHit hit;
            Vector3 direction = transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(transform.position, direction, Color.red,0.5f, true);
            RaycastHit hit;

            int layerMask = 1 << LayerMask.NameToLayer("Ammo");

            if (Physics.Raycast(transform.position, direction, out hit, 0.5f, layerMask))
            {
                AmmoHandler ammoScript = hit.collider.gameObject.GetComponent<AmmoHandler>();
                if (ammoScript != null)
                {
                    currentMaterial = ammoScript.GetMaterial();
                }
            }
            this.transform.GetComponent<BulletHandler>().SetMaterial(currentMaterial); 
        }
    }
    /* private void OnCollisionEnter(Collision other) {
        Debug.Log("Collision");
        Debug.Log(other);
        if (other.gameObject.CompareTag("Ammo")) {
            Debug.Log("Ammo");
            this.transform.GetComponent<BulletHandler>().SetMaterial(other.gameObject.transform.GetComponent<AmmoHandler>().GetMaterial()); 
        }
    }*/
} 
