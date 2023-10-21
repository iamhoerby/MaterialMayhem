using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
   public customMaterial currentMaterial; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }
    public customMaterial GetMaterial() {
        return currentMaterial; 
    }
    public void SetMaterial(customMaterial material) {
        currentMaterial = material;
    }
}
