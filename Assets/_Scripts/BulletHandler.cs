using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public int currentMaterial = 0; //0: Default; 1: Honey; 2: Rubber; 3: Ice; 4: Metal; 5: Cardbord 

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
    public int GetMaterial() {
        return currentMaterial; 
    }
    public void SetMaterial(int material) {
        currentMaterial = material;
    }
}
