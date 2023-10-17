using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialImpactHandler : MonoBehaviour
{
    public float impactLimit = 3.0f; 

    public float defaultMass = 1.0f;
    public float metalMass = 10.0f; 
    public float cardbordMass = 0.1f;
    public Material defaultMaterial; 
    public Material honeyMaterial; 
    public Material rubberMaterial; 
    public Material iceMaterial; 
    public Material metalMaterial; 
    public Material cardbordMaterial; 
    //public PhysicMaterial defaultPhysic;
    //public PhysicMaterial honeyPhysic;
    public PhysicMaterial rubberPhysic;
    public PhysicMaterial icePhysic;
    //public PhysicMaterial metalPhysic;
    //public PhysicMaterial cardbordPhysic;
    
    private int currentMaterial; //0: Default; 1: Honey; 2: Rubber; 3: Ice; 4: Metal; 5: Cardbord 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Bullet")) {
            int newMaterial = other.gameObject.GetComponent<BulletHandler>().GetMaterial(); 
            if (newMaterial != currentMaterial) {
                currentMaterial = newMaterial;
                UpdateMaterial(); 
            }
        } else {
            GetImpactEffect(other.gameObject);
        }
    }
    void UpdateMaterial() {
        switch (currentMaterial)
        {
            case 0: 
                setDefault();
                break;
            case 1: 
                setHoney(); 
                break;
            case 2: 
                setRubber();
                break;
            case 3: 
                setIce();
                break;
            case 4: 
                setMetal();
                break;
            case 5: 
                setCardbord();
                break;
            default: 
                setDefault(); 
                break; 
        }
    }

    void setDefault() {
        gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
        gameObject.GetComponent<Collider>().material = null; 
        gameObject.GetComponent<Rigidbody>().mass = defaultMass; 
    }
    void setHoney() {
        //TODO
        /* gameObject.GetComponent<MeshRenderer>().material = honeyMaterial;
        gameObject.GetComponent<Collider>().material = null; 
        gameObject.GetComponent<Rigidbody>().mass = defaultMass;  */
    }
    void setRubber() {
        gameObject.GetComponent<MeshRenderer>().material = rubberMaterial;
        gameObject.GetComponent<Collider>().material = rubberPhysic; 
        gameObject.GetComponent<Rigidbody>().mass = defaultMass; 
    }
    void setIce() {
        gameObject.GetComponent<MeshRenderer>().material = iceMaterial;
        gameObject.GetComponent<Collider>().material = icePhysic; 
        gameObject.GetComponent<Rigidbody>().mass = defaultMass; 
    }
    void setMetal() {
        gameObject.GetComponent<MeshRenderer>().material = metalMaterial;
        gameObject.GetComponent<Collider>().material = null; 
        gameObject.GetComponent<Rigidbody>().mass = metalMass; 
    }
    void setCardbord() {
        gameObject.GetComponent<MeshRenderer>().material = cardbordMaterial;
        gameObject.GetComponent<Collider>().material = null; 
        gameObject.GetComponent<Rigidbody>().mass = cardbordMass; 
    }
    void GetImpactEffect(GameObject other) {
        switch (currentMaterial)
        {
            case 0: 
                //defaultEffect(other);
                break;
            case 1: 
                //honeyEffect(other); 
                break;
            case 2: 
                //rubberEffect(other);
                break;
            case 3: 
                //iceEffect(other);
                break;
            case 4: 
                //metalEffect(other);
                break;
            case 5: 
                cardbordEffect(other);
                break;
            default: 
                //defaultEffect(other); 
                break; 
        }
    }
    private void cardbordEffect(GameObject other) {
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
