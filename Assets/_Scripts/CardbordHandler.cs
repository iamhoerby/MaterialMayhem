using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardbordHandler : MonoBehaviour
{
    public float impactLimit = 3.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void OnCollisionEnter(Collision other) {
        GetImpactEffect(other.gameObject);
    }
    void GetImpactEffect(GameObject other) {
        Vector3 otherVelocity = other.GetComponent<Rigidbody>().velocity; 
        Vector3 velocityDifference = gameObject.GetComponent<Rigidbody>().velocity - otherVelocity;  
        Vector3 direction = gameObject.transform.position - other.transform.position; 
        if (direction.y < 0 && other.GetComponent<Rigidbody>().mass >= 1) {
            Destroy(gameObject);
        }
        if (Mathf.Abs(velocityDifference.magnitude) >= impactLimit) {
            Destroy(gameObject); 
        }
    }
}
