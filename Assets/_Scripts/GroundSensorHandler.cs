using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensorHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private customMaterial currentMaterial;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other) {
        Debug.Log("Collision");
        if (other.gameObject.CompareTag("Morphable")) {
                    Debug.Log("Morphable");

            currentMaterial = other.gameObject.GetComponent<MaterialImpactHandler>().GetMaterial(); 
        } else if (other.gameObject.CompareTag("Ground")) {
            currentMaterial = customMaterial.Default; 
        }
    }
    public void OnTriggerExit(Collider other) {
        Debug.Log("Exit");
        currentMaterial = customMaterial.None; 
    }
    public customMaterial GetMaterial() {
        return currentMaterial; 
    }
}
